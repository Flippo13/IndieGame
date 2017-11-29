using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacles : MonoBehaviour {

	protected virtual void OnTriggerEnter(Collider collider)
    {
        PlayerController_Felix player = collider.GetComponent<PlayerController_Felix>();
        if(player)
            OnHit(player);
    }

    protected virtual void OnTriggerStay(Collider collider)
    {
        PlayerController_Felix player = collider.GetComponent<PlayerController_Felix>();

        if (player)
            OnHit(player);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        PlayerController_Felix player = collision.collider.GetComponent<PlayerController_Felix>();

        if (player)
            OnHit(player);
    }

    protected abstract void OnHit(PlayerController_Felix player);
}
