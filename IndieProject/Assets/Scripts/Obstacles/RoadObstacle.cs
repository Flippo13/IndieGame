using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadObstacle : Obstacles
{

    public bool permObstacle; 

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnHit(PlayerController_Felix player)
    {
        //TODO: Call a function within player that slows the player down
        player.ObstacleHit();
        if(!permObstacle)
        Destroy(gameObject); 

    }
}
