using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    private enum Try { First, Second, Third };
    private Try numOfTry;

    private enum SpawnDirection { Right, Left }; 
    [SerializeField]
    private SpawnDirection spawnDirection;
    private float carDirection; 
        
    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private int spawnSpeed = 5; 

    [SerializeField]
    private List<GameObject> cars = new List<GameObject>();

    private int index;

    // Use this for initialization
    void Start () {
        numOfTry = Try.First;

        if (spawnDirection == SpawnDirection.Right)
            carDirection = 0;
        else if (spawnDirection == SpawnDirection.Left)
            carDirection = 180; 

        StartCoroutine("Spawning",spawnSpeed);
	}
	
	// Update is called once per frame
	void Update () {

	}

    IEnumerator Spawning(int spawnSpeed)
    {
        while (true)
        {
            for (int i = 0; i < cars.Count; i++)
            {
                yield return new WaitForSeconds(spawnSpeed);
                if (i > cars.Count - 1) i = 0;
                Vector3 goodSpawnPos = new Vector3(spawnPos.position.x, spawnPos.position.y + (cars[i].transform.localScale.y/2), spawnPos.position.z); 
                GameObject car = Instantiate(cars[i], goodSpawnPos, Quaternion.Euler(0,carDirection,0));
                //car.gameObject.transform.position = new Vector3 (car.transform.position.x, car.GetComponent<Collider>().bounds.size.y,car.transform.position.z);
                Physics.IgnoreCollision(this.GetComponent<Collider>(), car.GetComponent<Collider>()); 
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Car")
        {
            Destroy(collider.gameObject); 
        }
    }
}
