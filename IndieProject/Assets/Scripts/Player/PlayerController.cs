using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public Camera playerCamera;

    private CharacterController controller;
    private bool grounded { get { return controller.isGrounded; } }
    
    public bool Enabled;
    
    [Header("Player")]
    public float walkSpeed;
    public float runSpeed;
    public float speedUp;
    public float jump;
    [Range(0, 1)]
    public float friction;

    private float momentum;
    private Vector3 movement;

    [Header("Camera")]
    public float distance;
    [Range(-30, 30)]
    public float angle;
    [SerializeField]
    private bool _showCursor;
    public bool showCursor
    {
        get { return Cursor.visible; }
        set { Cursor.visible = value; }
    }

    private void OnValidate()
    {
        if (playerCamera == null) return;
        playerCamera.transform.rotation = Quaternion.Euler(angle, playerCamera.transform.rotation.eulerAngles.y, 0);
        MoveCamera();
    }

    void Awake () {
        controller = GetComponent<CharacterController>();
        Debug.Assert(controller != null, gameObject.name + ": No CharacterController found!");
        showCursor = _showCursor;
    }

    private void FixedUpdate()
    {
        if (Enabled)
        {
            MovePlayer();
            MoveCamera();
        }
    }


    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (movement.z < walkSpeed) movement.z = walkSpeed;
            if (movement.z < runSpeed) movement.z += speedUp;
        }
        if (Input.GetKey(KeyCode.S)) movement.z = -walkSpeed;
        if (Input.GetKey(KeyCode.A)) movement.x = -walkSpeed;
        if (Input.GetKey(KeyCode.D)) movement.x = walkSpeed;

        if (grounded)
        {
            if (Input.GetKey(KeyCode.Space)) movement.y += jump;
        }
        else
        {
            movement += Physics.gravity * Time.deltaTime;
        }

        movement *= friction;

        controller.Move(movement);

        /**Basic Movement
        float delta;

        movement.x += Input.GetAxis("Horizontal") * runSpeed * Time.deltaTime;
        movement.z += Input.GetAxis("Vertical") * runSpeed * Time.deltaTime;

        movement *= friction;

        controller.Move(movement);
        if (grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                movement.y += Input.GetAxis("Jump") * jump;
            }
            delta = runSpeed;
        }
        else
        {
            movement += Physics.gravity * Time.deltaTime;
            delta = airSpeed;
        }
        /**/
    }

    public void MoveCamera()
    {
        if (playerCamera == null) return;
        playerCamera.transform.position = transform.position - (playerCamera.transform.forward * distance);
    }
    
}
