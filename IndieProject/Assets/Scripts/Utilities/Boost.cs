using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Vehicles.Ball
{
    public class Boost : MonoBehaviour
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

        private void OnTriggerEnter()
        {
            ball.Boost(this.transform.position); 
        }
    }
}
