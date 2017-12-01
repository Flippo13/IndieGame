using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Car : Obstacles
{

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float speed = 4;

    [SerializeField]
    private bool isObstacle; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!isObstacle)
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self); 
    }

    protected override void OnHit(PlayerController_Felix player)
    {
        player.ObstacleHit();
        Destroy(this.gameObject); 
        //Call function within player to lower players speed and maker player blink and be invincible for a few seconds
    }
}
