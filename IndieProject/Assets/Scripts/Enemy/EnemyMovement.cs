using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private Vector3 velocity; 

    [SerializeField]
    private int maxDistanceFromPlayer; 
    private Vector3 distanceFromPlayer;


    private Rigidbody rb; 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        distanceFromPlayer = transform.position - player.transform.position;
        //Debug.Log("Distance from Player "+ distanceFromPlayer.magnitude);
        //Debug.Log("Velocity " + rb.velocity.magnitude); 
        Move();
	}

    private void Move()
    {
        if (distanceFromPlayer.magnitude > maxDistanceFromPlayer && rb.velocity.magnitude > 2)
            rb.AddForce(moveDirection * -speed); 
       else if(rb.velocity.magnitude < 12f) 
           rb.AddForce(moveDirection * speed); 
    }
}
