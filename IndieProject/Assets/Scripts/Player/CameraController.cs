using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject Follow;
    
    public float distance;
    [Range(-30, 30)]
    public float angle;
    [Range(0, 1)]
    public float lerp;

    private void OnValidate()
    {
        if (distance < 0) distance = 0;
        RefreshCamera();
    }
    
	void FixedUpdate () {
        RefreshCamera();
    }

    void RefreshCamera()
    {
        Quaternion targetRotation = Quaternion.Euler(angle, Follow.transform.rotation.eulerAngles.y, 0);
        transform.rotation = targetRotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerp);

        Vector3 targetPosition = Follow.transform.position - (transform.forward * distance);
        transform.position = Vector3.Lerp(transform.position, targetPosition, lerp);
    }
}
