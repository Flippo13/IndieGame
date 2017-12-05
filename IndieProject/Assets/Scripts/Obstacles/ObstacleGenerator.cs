using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour {
    
    public float laneWidth;
    public uint lanes;
    public uint length;
    public uint size { get { return lanes * length; } }

    const float HALF = 0.5f;

    [Range(0, 5)] public float emptySpaces;
    [Range(0, 1)] public float obstacles;
    [Range(0, 1)] public float acorns;
    [Range(0, 1)] public float peanuts;
    [Range(0, 1)] public float seeds;

    private GameObject parentObj;
    public GameObject[] Obstacles;
    public PeanutCollectable Acorn;
    public PeanutCollectable Peanut;
    public PeanutCollectable Seed;

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
        transform.localScale = new Vector3(lanes * laneWidth, -1, length);
    }
    
    void GenerateObstacles()
    {
        parentObj = new GameObject("Obstacle Container");
        parentObj.transform.position = transform.position + (Vector3.left * laneWidth * HALF);

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
<<<<<<< HEAD
              //  switch(Lotto())
=======
                GameObject obj = null;
                int lotto = Lotto(emptySpaces, obstacles, acorns, peanuts, seeds);
                Debug.Log(lotto);
                switch (lotto)
                {
                    case 4: obj = Instantiate(Seed, parentObj.transform).gameObject; break;
                    case 3: obj = Instantiate(Peanut, parentObj.transform).gameObject; break;
                    case 2: obj = Instantiate(Acorn, parentObj.transform).gameObject; break;
                    case 1: obj = Instantiate(Obstacles[Random.Range(0, Obstacles.Length)], parentObj.transform); break;
                    default: Debug.LogFormat("Error!"); break;
                }
                if(obj != null)
                {
                    obj.transform.localPosition = new Vector3(b * laneWidth, 1, a);
                }
                emptySpots[a * lanes + b] = true;
>>>>>>> Alex_Branch
            }
        }
    }
	
	void Update ()
    {

    }
    
    public int Lotto(params float[] nums)
    {
        if (nums.Length == 0) return -1;
        float sum = 0;
        foreach(float f in nums) sum += f;
        
        float result = Random.Range(0, sum);
        for(int a = 0; a < nums.Length; a++)
        {
            result -= nums[a];
            if (result <= 0) return a;
        }
        return -1;
    }
}

