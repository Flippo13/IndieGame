using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacles : MonoBehaviour {

	protected virtual void OnTriggerEnter(Collider collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();
        if(player)
            OnHit(player);
    }

    protected virtual void OnTriggerStay(Collider collider)
    {
        PlayerController player = collider.GetComponent<PlayerController>();

        if (player)
            OnHit(player);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        PlayerController player = collision.collider.GetComponent<PlayerController>();

        if (player)
            OnHit(player);
    }

    protected abstract void OnHit(PlayerController player);
}
