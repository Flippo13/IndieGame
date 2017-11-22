using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Vehicles.Ball
{
    public class Boost : MonoBehaviour
    {
        [SerializeField]
        private Ball ball; 
        private enum Direction { right, left, forward, backward };

        [SerializeField]
        private float strength; 

        [SerializeField]
        private Direction dirStat; 

        private Vector3 dir; 

        // Use this for initialization
        void Start()
        {
            if (dirStat == Direction.right)
                dir = new Vector3(1, 0, 0);
            else if (dirStat == Direction.left)
                dir = new Vector3(-1, 0, 0);
            else if (dirStat == Direction.forward)
                dir = new Vector3(0, 0, 1); 
            else if (dirStat == Direction.backward)
                dir = new Vector3(0, 0, -1);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        private void OnCollisionStay(Collision other)
        {
            float playerSpeed = ball.Rigidbody.velocity.magnitude;
            Vector3 boost = dir * playerSpeed * strength;
            ball.Rigidbody.AddForce(boost, ForceMode.Impulse); 
        }
    }
}
