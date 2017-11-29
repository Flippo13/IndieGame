using UnityEngine;
using System.Collections;

public enum NutType { Peanut, Acorn, SunflowerSeed }
public class PeanutCollectable : MonoBehaviour
{
    public NutType nut;
    public AudioClip collectSound;
    public int worth;
    [HideInInspector]
    public Transform attractor = null;

    private float speed = 0;
    private Vector3 distance;

    public void Collect(GameObject collector)
    {
        attractor = collector.transform;
    }

    private void FixedUpdate()
    {
        if (attractor == null) return;
        speed += Time.deltaTime;
        distance = attractor.position - transform.position;
        if (distance.magnitude < 1) Destroy(gameObject);
        transform.position += distance.normalized * Mathf.Clamp(speed, 0, distance.magnitude);
    }
}
