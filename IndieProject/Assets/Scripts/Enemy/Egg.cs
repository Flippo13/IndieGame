using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private float speed; 

    private enum State {Obstacle, Falling, Homing};
    private State state; 

    private Vector3 distanceFromPlayer;
    private bool canHit;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        player = GameObject.Find("Player");
        enemy = GameObject.Find("Enemy");

        state = State.Falling; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.DrawRay(transform.position, enemy.transform.position - this.transform.position, Color.red);
        CheckPlayerPosition();
        if (Input.GetKeyDown(KeyCode.R))
            state = State.Homing; 
        if (state == State.Homing)
        {
            ShootEgg(); 
        }

	} 

    private void ShootEgg()
    {
/*
        rb.useGravity = false; 
        float distance = Mathf.Infinity;

        float diff = (enemy.transform.position - transform.position).sqrMagnitude;

        if(diff < distance)
        {
            distance = diff; 
        }

        rb.AddForce(transform.forward * speed,ForceMode.Force);
        Quaternion targetRotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, 10));
        */
        
        rb.useGravity = false; 
        Vector3 distanceFromEnemy = enemy.transform.position - this.transform.position;

        Vector3 dir = distanceFromEnemy.normalized;

        Quaternion rotation = Quaternion.LookRotation(distanceFromEnemy);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1); 

       rb.AddForce(dir * 50,ForceMode.Force); 
       
    }

    private void CheckPlayerPosition()
    {
        distanceFromPlayer = player.transform.position - this.transform.position; 

        if (distanceFromPlayer.magnitude < 5)
        {
            canHit = true; 
        }
        else
        {
            canHit = false; 
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" && canHit && state != State.Obstacle)
        {
            state = State.Homing; 
        }
        if (other.collider.tag == "Ground")
        {
            //state = State.Obstacle; 
        }
    }
}
