using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject enemy;

   // [SerializeField]
   // private float speed; 

    private enum State {Obstacle, Falling};
    private State state; 

    private Vector3 distanceFromPlayer;
    private bool canHit;

    [SerializeField]
    private GameObject[] spawnableObjects; 

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        player = GameObject.Find("Player 2.1 1");
        enemy = GameObject.Find("Enemy");

        state = State.Falling; 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.DrawRay(transform.position, enemy.transform.position - this.transform.position, Color.red);

        CheckPlayerPosition();
	} 

    private void CheckPlayerPosition()
    {
        distanceFromPlayer = player.transform.position - this.transform.position;

        if (distanceFromPlayer.magnitude < 20 && state == State.Obstacle)
            Break(); 

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Ground")
        {
            state = State.Obstacle; 
        }
    }

    private void Break()
    {
        int chosenObject = Random.Range(0, spawnableObjects.Length);
        //Play break animation
        GameObject spawnedObject = Instantiate(spawnableObjects[chosenObject], this.transform.position, Quaternion.identity);
        Destroy(gameObject); 
    }

}
