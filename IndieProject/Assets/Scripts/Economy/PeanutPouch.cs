using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PeanutPouch : MonoBehaviour
{
    [HideInInspector]
    public int peanuts;
    public Text indicator;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PeanutCollectable>() != null)
        {
            peanuts++;
            indicator.text = "Peanuts: " + peanuts;
            Destroy(other.gameObject);
        }
    }
}
