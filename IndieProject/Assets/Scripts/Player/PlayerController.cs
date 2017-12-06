using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController charC;

    private enum PlayerState { Default, Jumping, DoubleJumping, WallRunning, Braking };
    private PlayerState playerState;

    private Vector3 moveDir;
    [SerializeField]
    private float moveSpd = 0;
    [SerializeField]
    private float spdMulti;
    [SerializeField]
    private float maxSpd;
    [SerializeField]
    private float turnSpd;
    [SerializeField]
    private float maxTurnSpd;
    [SerializeField]
    private float brakeValue;
    [SerializeField]
    private float slownDownTransition; 
    [SerializeField]
    private float jumpStrength; 
    private Vector3 vel;
    private Vector3 acc;
    [SerializeField]
    private float gravity = 20f;
    private float x;
    private int jumpCount = 0;
    private float slowSpeed; 
    private RaycastHit wallHit;

    private void Start()
    {
        charC = GetComponent<CharacterController>();
        playerState = PlayerState.Default; 
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        PlayerInput(); 
        moveDir.y -= gravity * Time.deltaTime;
        charC.Move(moveDir * Time.deltaTime);

        switch (playerState)
        {
            case PlayerState.Default :
                MoveForward(); 
            break;
            case PlayerState.Braking :
                Brake();
                break;
        }
    }

    private void MoveForward()
    {
        if (moveSpd <= maxSpd)
        {
            x += Time.deltaTime;
            moveSpd = 3 * Mathf.Pow(spdMulti, x);
        }
       
        float turnSpdTime = moveSpd / maxSpd;
        if (turnSpdTime > 1)
            turnSpdTime = 1;
        turnSpd = Mathf.Lerp(0, maxTurnSpd, turnSpdTime);

        if (charC.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(Vector3.forward);
            moveDir *= moveSpd;
        }
    }

    private void PlayerInput()
    {
        if (Input.GetKey(KeyCode.A))
            Rotate(-1);
        if (Input.GetKey(KeyCode.D))
            Rotate(1);
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount <= 1)
        {
            turnSpd = 30;
            jumpCount++; 
            moveDir.y = jumpStrength;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerState = PlayerState.Braking; 
            slowSpeed = moveSpd / brakeValue;
        }
       
        if (jumpCount >= 2 && charC.isGrounded)
        {
            jumpCount = 0;
        }
    }

    private void CheckWallRun()
    {
        if (jumpCount > 0)
        {
            wallHit = WallCheck();
            if (wallHit.collider != null)
            {
                UpdateWallRun(); 
            }
        }
    }

    private void Brake()
    {
        slownDownTransition += Time.deltaTime * 1/3;
        if (slownDownTransition > 0)
        {
            moveSpd = Mathf.Lerp(moveSpd, slowSpeed, slownDownTransition);
            x -= Time.deltaTime * 1 / 3;
        }
        if (slownDownTransition > 1.5f)
        {
            slownDownTransition = 0;
            playerState = PlayerState.Default; 
        }

        if (charC.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(Vector3.forward);
            moveDir *= moveSpd;
        }
    }

    private void Rotate(int dir)
    {
        transform.Rotate((transform.up * dir) * turnSpd * Time.deltaTime);

    }

    private void Jump()
    {
        jumpCount++; 

        if(jumpCount == 2)
            moveDir.y = jumpStrength * 1.2f;
        else

        if (charC.isGrounded)
        {
            playerState = PlayerState.Default; 
        }

    }
    
    private RaycastHit WallCheck()
    {
        Ray rightRay = new Ray(transform.position, transform.TransformDirection(Vector3.right));
        Ray leftRay = new Ray(transform.position, -transform.TransformDirection(Vector3.left));

        RaycastHit wallHitRight;
        RaycastHit wallHitLeft;

        bool rightHit = Physics.Raycast(rightRay.origin, rightRay.direction, out wallHitRight, 2f);
        bool leftHit =  Physics.Raycast(leftRay.origin, leftRay.direction, out wallHitLeft, 2f);

        Debug.Log(rightHit + " " + leftHit); 

        if (rightHit && Vector3.Angle(transform.TransformDirection(Vector3.forward),wallHitRight.normal ) > 90)
        {
            return wallHitRight; 
        }
        else
        if (leftHit && Vector3.Angle(transform.TransformDirection(Vector3.forward), wallHitLeft.normal) > 90)
        {
            wallHitLeft.normal *= -1; 
            return wallHitRight;
        }
        else
            return new RaycastHit(); 
    }
    
    private void UpdateWallRun()
    {
        if (charC.isGrounded)
        {
             wallHit = WallCheck();
            if (wallHit.collider == null)
            {
                StopWallRun();
                return;
            }
            Debug.Log("Wall running"); 
            float lastJumpHeight = moveDir.y;

            Vector3 crossVec = Vector3.Cross(wallHit.normal, -Vector3.up);

            Quaternion lookRotation = Quaternion.LookRotation(crossVec);
            transform.rotation = lookRotation;

            moveDir = crossVec;
            moveDir.Normalize();
            moveDir *= moveSpd;
        }
    }

    private void StopWallRun()
    {
        throw new NotImplementedException();
    }
}