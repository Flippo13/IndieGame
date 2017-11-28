using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacles : MonoBehaviour {

	protected virtual void OnTriggerEnter(Collider collider)
    {
        PhysPlayerController player = collider.GetComponent<PhysPlayerController>();
        if(player)
            OnHit(player);
    }

    protected virtual void OnTriggerStay(Collider collider)
    {
        PhysPlayerController player = collider.GetComponent<PhysPlayerController>();
        if (player)
            OnHit(player);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        PhysPlayerController player = collision.collider.GetComponent<PhysPlayerController>();
        if (player)
            OnHit(player);
    }

    protected abstract void OnHit(PhysPlayerController player);
}
