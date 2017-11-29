using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public Vector3 axis;
    public float RPM;
    
	void FixedUpdate () {
        transform.Rotate(axis * RPM * 6 * Time.deltaTime);
	}
}
