using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhysPlayerController : MonoBehaviour {

    const float OFFMARGIN = 0.1f;

    private Rigidbody rbody;

    public bool Enabled;
    public bool DebugMode;
    public Text DebugTexts;

    [Header("Player")]
    public bool UseFakeGravity;
    public Vector3 fakeGravity;
    
    public float startSpeed;
    public float maxSpeed;
    public float speedUp;
    public float jumpHeight;
    public float doubleJumpModifier;
    public float wallGripStrength;
    public float wallGripStamina;

    [Header("Camera")]
    public Camera playerCamera;
    public float distance;
    [SerializeField]
    [Range(-45, 45)]
    private float _angle;
    [SerializeField]
    private bool _showCursor;

    public float angle
    {
        get { return playerCamera.transform.rotation.eulerAngles.z; }
        set
        {
            playerCamera.transform.rotation = Quaternion.Euler(value, playerCamera.transform.rotation.eulerAngles.y, 0);
            MoveCamera();
        }
    }
    public bool showCursor
    {
        get { return Cursor.visible; }
        set { Cursor.visible = value; }
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
                direction = -transform.forward;
                return true;
            }
            return false;
    }
    
    /// <param name="offMargin">Distance above the collider that still counts as grounded</param>
    private bool isGrounded(float offMargin)
    {
        return Physics.Raycast(transform.position, -transform.up, 1 + offMargin);
    }
    private bool isGrounded(float offMargin, out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, -transform.up, out hit, 1 + offMargin);
    }

    private float _wallGrip;
    private bool _doubleJump;
    private float _currentSpeed;
    private Vector3 _gravity { get { return UseFakeGravity ? fakeGravity : Physics.gravity; } }
    private bool _isRunnig = false;
    private float groundSpeed {
        get { return new Vector2(rbody.velocity.x, rbody.velocity.z).magnitude; }
        set
        {
            Vector2 s = new Vector2(rbody.velocity.x, rbody.velocity.z).normalized * value;
            rbody.velocity = new Vector3(s.x, rbody.velocity.y, s.y);
        }
    }
    private float globalSpeed {
        get { return rbody.velocity.magnitude; }
        set { rbody.velocity = rbody.velocity.normalized * value; }
    }
    private bool controlPadPressed
    {
        get { return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A); }
    }

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();
        if (rbody == null) gameObject.AddComponent<Rigidbody>();
        Debug.Assert(rbody != null, gameObject.name + ": No RigidBody Attached.");
        Debug.Assert(playerCamera != null, gameObject.name + ": No Camera Attached.");
    }

    void Start()
    {
        _currentSpeed = startSpeed;
        _wallGrip = wallGripStamina;
    }

    private void FixedUpdate()
    {
        if (isGrounded(OFFMARGIN))
        {
            _wallGrip = wallGripStamina;
            _doubleJump = true;
        }
        DebugTexts.text = "";
        if (Enabled)
        {
            AllignToGravity();
            MovePlayer();
            WallRun();
            MoveCamera();
        }
        if (DebugMode)
        {
            DebugTexts.text += string.Format("\nPlayer Velocity: [{0}]\nSpeed: {1}m/s", rbody.velocity.ToString(), Mathf.Floor(globalSpeed));
        }
    }

    public void MoveCamera()
    {
        if (playerCamera == null) return;
        playerCamera.transform.position = transform.position - (playerCamera.transform.forward * distance);
    }

    private void AllignToGravity()
    {

    }

    Vector3 _movement;
    private void MovePlayer()
    {
        if (controlPadPressed)
        {
            if(_currentSpeed < maxSpeed) _currentSpeed += speedUp * Time.deltaTime;
        }
        else
        {
            if(_currentSpeed > startSpeed) _currentSpeed -= speedUp * 10 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rbody.AddForce(transform.forward, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rbody.AddForce(-transform.forward, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rbody.AddForce(transform.right, ForceMode.VelocityChange);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rbody.AddForce(-transform.right, ForceMode.VelocityChange);
        }
        if(groundSpeed > _currentSpeed) groundSpeed = _currentSpeed;

        if (Input.GetKeyDown(KeyCode.Space)) CallJump();
        
    }

    private void CallJump()
    {
        RaycastHit hit;
        if (isGrounded(OFFMARGIN, out hit))
        {
            Jump(jumpHeight);
            return;
        }
        else if(_doubleJump)
        {
            _doubleJump = false;
            Jump(jumpHeight * doubleJumpModifier);
        }
    }

    private void Jump(float force)
    {
        rbody.AddForce(transform.up * force, ForceMode.VelocityChange);
    }

    private void WallRun()
    {
        //print(_wallGrip);
        Vector3 direction;
        if (!isGrounded(OFFMARGIN) && isOnWall(OFFMARGIN, out direction))
        {
            if(_wallGrip > 0)
            {
                _wallGrip -= Time.deltaTime;
                float forceOnWall = Vector3.Dot(direction, rbody.velocity);
                rbody.AddForce(-direction * wallGripStrength, ForceMode.VelocityChange);
                rbody.AddForce(transform.up * forceOnWall, ForceMode.VelocityChange);
            }
            else
            {
                rbody.AddForce(-direction * wallGripStrength * 2, ForceMode.VelocityChange);
            }
        }
    }
    
    /*
    private void LimitVelocity()
    {
        Vector2 speed = new Vector2(rbody.velocity.x, rbody.velocity.z);
        speed = speed.normalized * _currentSpeed;
        //rbody.velocity = Vector3.Lerp(rbody.velocity, new Vector3(speed.x, rbody.velocity.y, speed.y), turnSpeed);
        rbody.velocity = new Vector3(speed.x, rbody.velocity.y, speed.y);
    }
    /*
    void MoveInDirection(KeyCode key, Vector3 direction)
    {
        if (Input.GetKey(key))
        {
            if (isRunnig)
            {
                rbody.AddForce(direction * runSpeed, ForceMode.VelocityChange);
                if (Speed2D < walkSpeed)
                {
                    isRunnig = false;
                }
                _warmup = 0;
            }
            else
            {
                rbody.AddForce(direction * walkSpeed, ForceMode.VelocityChange);
                if (_warmup >= speedUpTime)
                {
                    rbody.AddForce(direction * runSpeed / 2, ForceMode.Impulse);
                    isRunnig = true;
                }
                _warmup += Time.deltaTime;
            }
        }
    }
    /**
    private void OnCollisionEnter(Collision collision)
    {
        for(int a = 0; a < collision.contacts.Length; a++)
        {
            print(collision.gameObject + ": " + (transform.position - collision.contacts[a].point));
        }
    }
    /**/
}
