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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World); 
    }

    protected override void OnHit(PlayerController player)
    {
        Debug.Log("Player hit Obstacle");
        Vector3 hitDir = player.transform.position - transform.position;
        hitDir = hitDir.normalized;
        player.ObstacleHit(hitDir);
        //Call function within player to lower players speed and maker player blink and be invincible for a few seconds
    }
}
