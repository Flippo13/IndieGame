using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_Felix : MonoBehaviour
{

    const float OFFMARGIN = 0.2f;

    private Rigidbody rbody;

    public bool Enabled;
    public bool DebugMode;
    public Text DebugTexts;

    public enum MovementState { Normal, WallRun, Tunnel }
    private MovementState state;

    [Header("Normal Movement")]
    //public bool UseFakeGravity;
    //public Vector3 fakeGravity;

    public float slowSpeed;
    public float setRunSpeed;
    private float runSpeed; 
    public float turnSpeed;
    public float acceleration;
    public float jumpPower;

    public float doubleJumpModifier;

    [Header("WallRun")]
    public float wallRunDistance;
    [Range(0.05f, 0.95f)]
    public float wallRunStrength;
    public float wallRunStamina;
    public float wallRunSpeed;
    public float wallJump;

    private bool exaust;
    private float _currentSpeed;
    public float _maxSpeed;
    private float _wallGrip;
    private bool _doubleJump;
    private float invincibilityTime;

    #region 
    //private Vector3 _gravity { get { return UseFakeGravity ? fakeGravity : Physics.gravity; } }
    private float groundSpeed
    {
        get { return new Vector2(rbody.velocity.x, rbody.velocity.z).magnitude; }
        set
        {
            Vector2 s = new Vector2(rbody.velocity.x, rbody.velocity.z).normalized * value;
            rbody.velocity = new Vector3(s.x, rbody.velocity.y, s.y);
        }
    }
    private float globalSpeed
    {
        get { return rbody.velocity.magnitude; }
        set { rbody.velocity = rbody.velocity.normalized * value; }
    }
    private bool controlPadPressed
    {
        get { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A); }
    }

    public Rigidbody RigidBody
    {
        get { return rbody; }
    }

    public bool invincibility { get; private set; }

    /// <summary>Returns </summary>
    /// <returns>0: - No Wall. 1: Wall on Right. 2: Wall on Left.</returns>
    private int isOnWall(out Vector3 wallNormal)
    {
        RaycastHit hit;
        wallNormal = Vector3.zero;
        bool[] side = new bool[2] { false, false };
        for (int s = 0; s < side.Length; s++)
        {
            Vector3 back = (transform.right * (2 * s - 1) - transform.forward).normalized;
            Vector3 front = (transform.right * (2 * s - 1) + transform.forward).normalized;
            side[s] =
                Physics.Raycast(transform.position, back, wallRunDistance) &&
                Physics.Raycast(transform.position, front, wallRunDistance);
            if (Physics.Raycast(transform.position, front, out hit, wallRunDistance))
            {
                Vector3 disToWall = hit.collider.ClosestPoint(transform.position) - transform.position;
                if (wallNormal == Vector3.zero || disToWall.magnitude < wallNormal.magnitude)
                    wallNormal = disToWall;
            }
        }
        if (side[0]) return 1;
        else
        if (side[1]) return 2;
        else
            return 0;
    }

    private bool isOnWall(float offMargin)
    {
        Vector3 dir;
        return isOnWall(offMargin, out dir);
    }
    private bool isOnWall(float offMargin, out Vector3 direction)
    {
        RaycastHit hit;
        return isOnWall(offMargin, out direction, out hit);
    }
    private bool isOnWall(float offMargin, out Vector3 direction, out RaycastHit hit)
    {
        direction = Vector3.zero;
        if (Physics.Raycast(transform.position, transform.right, out hit, 0.5f + offMargin))
        {
            direction = transform.right;
            return true;
        }
        if (Physics.Raycast(transform.position, -transform.right, out hit, 0.5f + offMargin))
        {
            direction = -transform.right;
            return true;
        }
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.5f + offMargin))
        {
            direction = transform.forward;
            return true;
        }
        return false;
    }
    private bool isGrounded(float offMargin)
    {
        return Physics.Raycast(transform.position, -transform.up, 1 + offMargin);
    }
    private bool isGrounded(float offMargin, out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, -transform.up, out hit, 1 + offMargin);
    }
    #endregion

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        if (rbody == null) gameObject.AddComponent<Rigidbody>();
        Debug.Assert(rbody != null, gameObject.name + ": No RigidBody Attached.");
    }

    void Start()
    {
        //_maxSpeed = runSpeed;
        _wallGrip = 0;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(transform.position, rbody.velocity * 4, Color.blue);
        if (isGrounded(OFFMARGIN))
        {
            _doubleJump = true;
        }

        if (invincibility)
        {
            invincibilityTime -= Time.deltaTime; 
            if(invincibilityTime <= 0)
            {
                invincibility = false; 
            }
        }

        DebugTexts.text = "";
        if (Enabled)
        {
            AllignToGravity();
            MovePlayer();
        }
        if (DebugMode)
        {
            DebugTexts.text += string.Format("\nPlayer Velocity: [{0}]\nSpeed: {1}m/s", rbody.velocity.ToString(), Mathf.Floor(globalSpeed));
        }
    }

    private void AllignToGravity()
    {

    }

    private void MovePlayer()
    {
        Vector3 wallNormal;
        int side = isOnWall(out wallNormal);
        state = MovementState.Normal;

        if (!isGrounded(OFFMARGIN) && side != 0 && _wallGrip < wallRunStamina && !exaust)
        {
            state = MovementState.WallRun;

            rbody.velocity = Vector3.Cross(wallNormal * (side == 2 ? 1 : -1), transform.up).normalized * rbody.velocity.magnitude;
        }
        if (_wallGrip > 0 && state != MovementState.WallRun) _wallGrip -= Time.deltaTime * wallRunStrength; else exaust = false;
        //print("Grip: " + _wallGrip);
        //print("State " + state);

        switch (state)
        {
            case MovementState.Normal: NormalMovement(); break;
            case MovementState.WallRun: WallMovement(wallNormal, side); break;
            case MovementState.Tunnel: TunnelMovement(); break;
        }
        print(transform.forward);
        //rbody.AddForce(transform.forward * runSpeed, ForceMode.Acceleration);
    }

    private void NormalMovement()
    {
        if (isGrounded(OFFMARGIN))
        {
            if (Input.GetKey(KeyCode.D)) transform.Rotate(transform.up * turnSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.A)) transform.Rotate(-transform.up * turnSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S)) runSpeed = slowSpeed; else runSpeed = setRunSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) CallJump();

        if (rbody.velocity.magnitude < _maxSpeed)
        {
            rbody.AddForce(transform.forward * runSpeed, ForceMode.Acceleration);
        }
    }

    private void WallMovement(Vector3 wallNormal, int side)
    {
        //print(_wallGrip);
        _wallGrip += Time.deltaTime / wallRunStrength;

        int wSide = side == 2 ? 1 : -1;
        //wallNormal *= wSide;

        Vector3 forward = Vector3.Cross(wallNormal * wSide, transform.up).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(forward, transform.up), OFFMARGIN);
        //transform.rotation = Quaternion.LookRotation(forward, transform.up);


        float offDistance = OFFMARGIN + 0.5f - wallNormal.magnitude;
        if (offDistance > 0)
        {
            transform.position += wallNormal.normalized * offDistance;
        }

        KeyCode grabKey = KeyCode.A;
        KeyCode letGoKey = KeyCode.D;

        if (side == 2)
        {
            grabKey = KeyCode.D;
            letGoKey = KeyCode.A;
        }
        Debug.Log("wallNormal " + wallNormal); 
        if (Input.GetKey(grabKey) && _wallGrip < wallRunStamina)
        {
            rbody.AddForce(transform.up * wallRunSpeed, ForceMode.Impulse);

            if (Vector3.Dot(wallNormal * wSide, rbody.velocity) > 0)
            {
                rbody.AddForce(-wallNormal * wSide, ForceMode.VelocityChange);
            }
        }
        if (Input.GetKey(letGoKey) || _wallGrip >= wallRunStamina)
        {
            exaust = true;

            rbody.velocity -= wallNormal * wSide * wallJump;
            state = MovementState.Normal;
        }
    }

    private void OnGUI()
    {
        GUI.TextField(new Rect(100, 60, 200, 20), rbody.velocity.ToString());
        GUI.TextField(new Rect(100, 80, 200, 20), rbody.velocity.magnitude.ToString());
    }

    private void TunnelMovement()
    {

    }

    private void CallJump()
    {
        RaycastHit hit;
        if (isGrounded(OFFMARGIN, out hit))
        {
            Jump(jumpPower);
            return;
        }
        else if (_doubleJump && Vector3.Dot(transform.up, rbody.velocity) < 2f)
        {
            _doubleJump = false;
            Jump(jumpPower * doubleJumpModifier);
        }
    }

    private void Jump(float force)
    {
        rbody.AddForce(transform.up * force, ForceMode.Impulse);
    }

    public void ObstacleHit()
    {
        //TODO: Play ObstacleHit Animation
        invincibility = true; 
        rbody.velocity = Vector3.zero; 
        rbody.AddForce(-transform.forward * 10, ForceMode.VelocityChange); 
    }
}