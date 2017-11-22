using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Vehicles.Ball
{
    public class DimensionHole : MonoBehaviour
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

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player")
            {
                 
            }
        }
    }
}
