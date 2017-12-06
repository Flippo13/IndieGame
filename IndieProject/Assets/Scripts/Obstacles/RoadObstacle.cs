using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadObstacle : Obstacles
{
    private enum State {  Default, Birth};
    private State state;
    public bool birth; 
    private Vector3 normalScale;
    private float transition;
    // Use this for initialization
    void Start()
    {
        normalScale = transform.localScale; 

    }

    // Update is called once per frame
    void Update()
    {
        if (birth)
            Birth(); 
    }

    public void Birth()
    {
        transition += Time.deltaTime * 1/0.3f;
      transform.localScale =  Vector3.Lerp(Vector3.zero, normalScale, transition); 
    }

    protected override void OnHit(PlayerController player)
    {
        //TODO: Call a function within player that slows the player down
        Debug.Log("Hit");
        Vector3 hitDir = player.transform.position - transform.position;
        hitDir = hitDir.normalized;
        player.ObstacleHit(hitDir); 
        
        //Destroy(gameObject); 
    }
}
