using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementOnRail : MonoBehaviour {

    public EnemyPath path;
    public GameObject player;
    public GameObject baby; 

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
        distToNextNode = 1; 
	}
	
	// Update is called once per frame
	void Update () {
        if (!path)
            return;
        if (!isCompleted)
            FollowPath();
        
        if(path.EndOfPath(currentSeg))
        {
            DropBaby(); 
        }

        if (Input.GetKeyDown(KeyCode.G))
            DropBaby(); 

        distanceFromPlayer = transform.position - player.transform.position;
    }

    private void FollowPath()
    {
        float distCovered = (Time.time - startTime) * speed;
        float distToGo = distCovered / distToNextNode;

        if (distToGo > 1)
        {
            if (distanceFromPlayer.magnitude > maxDistanceFromPlayer)
                speed = 5;
            else if (distanceFromPlayer.magnitude < maxDistanceFromPlayer)
                speed = 25; 
            distToGo = 0;
            currentSeg++;
            startTime = Time.time; 
            distToNextNode = path.DistToNextNode(currentSeg);  
        }

        else if (distToGo < 0)
        {
            if (distanceFromPlayer.magnitude > maxDistanceFromPlayer)
                speed = 15;
            else if (distanceFromPlayer.magnitude < maxDistanceFromPlayer)
                speed = 25;
            distToGo = 1; 
            currentSeg--;
            startTime = Time.time; 
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

    private void DropBaby()
    {
        baby.transform.parent = null;
        baby.GetComponent<Rigidbody>().useGravity = true; 
    }
}
