using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class DimensionHole : MonoBehaviour
    {
    



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
