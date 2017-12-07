using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour {

    private enum SpawnDirection {Right,Left,Forward,Backward}; 
    [SerializeField]
    private SpawnDirection spawnDirection;
    private float carDirection;
    public bool isDestination; 
    [SerializeField]
    private Transform spawnPos;

    [SerializeField]
    private int spawnSpeed = 5; 

    [SerializeField]
    private List<GameObject> cars = new List<GameObject>();

    private int index;

    // Use this for initialization
    void Start () {

        if (!isDestination)
        {
            if (spawnDirection == SpawnDirection.Right)
                carDirection = 270;
            else if (spawnDirection == SpawnDirection.Left)
                carDirection = 0;


            StartCoroutine("Spawning", spawnSpeed);
        }
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
                GameObject car = Instantiate(cars[i], goodSpawnPos,Quaternion.Euler(new Vector3 (0,carDirection,0)));
                //car.transform.rotation = Quaternion.Lerp(car.transform.rotation,otherSpawner)
                //car.gameObject.transform.position = new Vector3 (car.transform.position.x, car.GetComponent<Collider>().bounds.size.y,car.transform.position.z);
                //Physics.IgnoreCollision(this.GetComponent<Collider>(), car.GetComponent<Collider>()); 
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
