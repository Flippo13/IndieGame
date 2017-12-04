using UnityEngine;
using System.Collections;

public enum NutType { Peanut, Acorn, Seed }
public class PeanutCollectable : MonoBehaviour
{
    public NutType type;
    public AudioClip pickupSound;
    public int worth;
    public Transform follow = null;
    private float attract = 0;
    
    public void OnCollect(PeanutPouch collector)
    {
        follow = collector.transform;
        collector.Add(type, worth);
        AudioSource.PlayClipAtPoint(pickupSound, transform.position);
    }

    private void FixedUpdate()
    {
        if (follow == null) return;
        attract += Time.deltaTime;

        Vector3 distance = follow.position - transform.position;
        if (distance.magnitude < 1f) Destroy(gameObject);
        transform.position += distance.normalized * Mathf.Clamp(attract, 0, distance.magnitude);
    }
}
