using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject egg;
    [SerializeField]
    private Transform spawnPos; 

    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private Vector3 velocity;

    [SerializeField]
    private int maxDistanceFromPlayer;
    private Vector3 distanceFromPlayer;

    [SerializeField]
    private int setDropTime;
    private float dropTime; 

    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dropTime = setDropTime; 
    }

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = transform.position - player.transform.position;
        //Debug.Log("Distance from Player "+ distanceFromPlayer.magnitude);
        //Debug.Log("Velocity " + rb.velocity.magnitude); 
        if(distanceFromPlayer.magnitude < 20f) 
        dropTime -= Time.deltaTime;

        if (dropTime <= 0)
        {
            DropEgg();
            dropTime = setDropTime; 
        }
        Move();
    }

    private void Move()
    {
        if (distanceFromPlayer.magnitude > maxDistanceFromPlayer && rb.velocity.magnitude > 2)
            rb.AddForce(moveDirection * -speed);
        else if (rb.velocity.magnitude < 12f)
            rb.AddForce(moveDirection * speed);
    }

    private void DropEgg() 
    {
        var droppedEgg =  Instantiate(egg, null, true);
        droppedEgg.transform.position = spawnPos.position; 
        Debug.Log(droppedEgg.transform.position); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Egg")
        {
            Debug.Log("Hit"); 
            Destroy(other); 
        }
    }

}
