using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector3 moveDirection;
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private GameObject egg;
    [SerializeField]
    private Transform spawnPos;
    [SerializeField]
    private Transform[] eggDropPos = new Transform[4]; 

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
        if(distanceFromPlayer.magnitude < 100f) 
        dropTime -= Time.deltaTime;

        if (dropTime <= 0)
        {
            ChooseDropPos(); 
            dropTime = setDropTime; 
        }
        Move();
    }

    private void Move()
    {
        if (distanceFromPlayer.magnitude > maxDistanceFromPlayer && rb.velocity.magnitude > 20)
            rb.AddForce(moveDirection * -speed);
        else if (rb.velocity.magnitude < 40f)
            rb.AddForce(moveDirection * speed);
    }


    private void ChooseDropPos()
    {
        int index = Random.Range(0, eggDropPos.Length);
        DropEgg(index); 
    }

    private void DropEgg(int position) 
    {
        Vector3 dropPos = eggDropPos[position].position; 

        var droppedEgg =  Instantiate(egg, null, true);
        droppedEgg.transform.position = new Vector3 (dropPos.x,spawnPos.position.y, spawnPos.position.z); 
    }

   
}
