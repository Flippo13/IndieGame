using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour {

    private Transform[] nodes; 

	// Use this for initialization
	void Start () {
        nodes = GetComponentsInChildren<Transform>(); 
	}
	
    public Vector3 LinearPosition(int seg, float ratio)
    {
            Vector3 p1 = nodes[seg].position;
            Vector3 p2 = nodes[seg + 1].position;
        
        return Vector3.Lerp(p1, p2, ratio); 
    }

    public Quaternion Orientation(int seg, float ratio)
    {
        Quaternion q1 = nodes[seg].rotation;
        Quaternion q2 = nodes[seg + 1].rotation;

        return Quaternion.Lerp(q1, q2, ratio); 
    }
    
    public float DistToNextNode(int seg)
    {
        return Vector3.Distance(nodes[seg].position, nodes[seg + 1].position); 
    }
    
    private void OnDrawGizmos()
    {
        /*
        Handles.color = Color.red; 
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            Handles.DrawDottedLine(nodes[i].position, nodes[i + 1].position, 2.0f); 
        }
        */
    }

    public bool EndOfPath(int currentSeg)
    {
        return currentSeg == nodes.Length - 2; 
    }
	// Update is called once per frame
	void Update () {
		
	}

}
