using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private float speed;

    private Animator anim; 

    private enum State {Obstacle, Falling};
    private State state; 

    private Vector3 distanceFromPlayer;
    private bool canHit;

    [SerializeField]
    private GameObject[] spawnableObjects;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>(); 
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.Find("Stork");

        state = State.Falling; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
       
        CheckPlayerPosition();
	} 

    private void CheckPlayerPosition()
    {
        distanceFromPlayer = player.transform.position - this.transform.position;

        if (distanceFromPlayer.magnitude < 40 && state == State.Obstacle)
            Break(); 

    }

    private void OnCollisionEnter(Collision other)
    {
            state = State.Obstacle; 
    }

    private void Break()
    {
        //Play break animation
        anim.SetTrigger("Break");
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Break") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
           int chosenObject = Random.Range(0, spawnableObjects.Length);
            GameObject spawnedObject = Instantiate(spawnableObjects[chosenObject], new Vector3(transform.position.x,transform.position.y + 1,transform.position.z), Quaternion.identity);
            spawnedObject.GetComponent<RoadObstacle>().birth = true; 
            Destroy(gameObject);
        }
    }

}
