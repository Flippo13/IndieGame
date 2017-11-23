using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Trampoline : MonoBehaviour
    {
        [SerializeField]
        private PhysPlayerController player; 
        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision other)
        {
            float playerSpeed = player.Rigidbody.velocity.magnitude;
            Vector3 launchDirection = transform.up;
            Vector3 launchPower = launchDirection * playerSpeed * 0.1f;
            player.Rigidbody.AddForce(launchPower, ForceMode.Impulse); 
        }
    }
