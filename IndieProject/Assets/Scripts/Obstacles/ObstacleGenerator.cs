﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
    
    public float laneWidth;
    public uint lanes;
    public uint length;

    const float HALF = 0.5f;

    [Range(0, 1)] public float obstacles;
    [Range(0, 1)] public float acorns;
    [Range(0, 1)] public float peanuts;
    [Range(0, 1)] public float seeds;

    private GameObject[] areas;

    private bool[] emptySpots;
    
    void Start () {
        BuildAreas();
        GenerateObstacles();
    }

    void BuildAreas()
    {
        areas = new GameObject[lanes];
        for (int a = 0; a < areas.Length; a++)
        {
            areas[a] = new GameObject("Area " + (a + 1));
            areas[a].transform.SetParent(transform);
            areas[a].transform.localScale = new Vector3(1 / (float)areas.Length, 1, 1);
            BoxCollider b = areas[a].AddComponent<BoxCollider>();
            b.isTrigger = true;
            areas[a].transform.localPosition = new Vector3(HALF - ((a + HALF) / areas.Length), 0, HALF);
        }
        transform.localScale = new Vector3(lanes * laneWidth, 1, length);
    }
    
    void GenerateObstacles()
    {
        emptySpots = new bool[lanes * length];
        for (int a = 0; a < length; a++)
        {
            for(int b = 0; b < lanes; b++)
            {
                emptySpots[a * lanes + b] = false;
            }
        }
        for (int a = 0; a < length; a++)
        {
            for (int b = 0; b < lanes; b++)
            {
                if (emptySpots[a * lanes + b]) continue;
              //  switch(Lotto())
            }
        }
    }
	
	void Update ()
    {

    }
    
    public int Lotto(float sum, params float[] nums)
    {
        float checkSum = 0;
        foreach(float f in nums) checkSum += f;
        if (checkSum > sum) return -1;

        float result = Random.Range(0, sum);
        for(int a = 0; a < nums.Length; a++)
        {
            result -= nums[a];
            if (result <= 0) return a;
        }
        return -1;
    }
}

