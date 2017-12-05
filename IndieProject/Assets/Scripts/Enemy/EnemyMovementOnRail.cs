using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementOnRail : MonoBehaviour {

    public EnemyPath path;
    public GameObject player; 

    private int currentSeg;
    private float transition;
    private bool isCompleted = false;

    private Rigidbody rb; 

    [SerializeField]
    private float speed;
    [SerializeField]
    private int maxDistanceFromPlayer; 

    private float startTime;
    private float distToNextNode;
    private Vector3 distanceFromPlayer;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        rb = GetComponent<Rigidbody>(); 
        //distToNextNode = path.DistToNextNode(currentSeg); 
	}
	
	// Update is called once per frame
	void Update () {
        if (!path)
            return;
        if (!isCompleted)
            FollowPath();
        else
            rb.useGravity = true; 

        distanceFromPlayer = transform.position - player.transform.position;


        if (distanceFromPlayer.magnitude > maxDistanceFromPlayer && speed > 5)
            speed--;
        else if (distanceFromPlayer.magnitude < maxDistanceFromPlayer && speed < 10)
            speed++; 

    }

    private void FollowPath()
    {
        Debug.Log(transform.position); 
        float distCovered = (Time.time - startTime) * speed;
        float distToGo = distCovered / distToNextNode; 

        if (distToGo > 1)
        {
            currentSeg++;
            startTime = Time.time; 
            distToGo = 0;
            distToNextNode = path.DistToNextNode(currentSeg);  
        }
        else if (distToGo < 0)
        {
            currentSeg--;
            startTime = Time.time; 
            distToGo = 1; 
            distToNextNode = path.DistToNextNode(currentSeg); 
        }

        if(path.EndOfPath(currentSeg))
        {
            Debug.Log("Path Complete"); 
            isCompleted = true; 
        }

        transform.position = path.LinearPosition(currentSeg, distToGo);
        transform.rotation = path.Orientation(currentSeg, distToGo); 
    }
}
