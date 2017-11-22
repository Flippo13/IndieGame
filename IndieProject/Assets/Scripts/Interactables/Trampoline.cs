using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Trampoline : MonoBehaviour
    {
        [SerializeField]
        private Ball ball; 
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
            float playerSpeed = ball.Rigidbody.velocity.magnitude;
            Vector3 launchDirection = transform.up;
            Vector3 launchPower = launchDirection * playerSpeed * 0.1f;
            ball.Rigidbody.AddForce(launchPower, ForceMode.Impulse); 
        }
    }
}
