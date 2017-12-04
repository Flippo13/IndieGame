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
        PeanutCollectable pc = other.GetComponent<PeanutCollectable>();
        if(pc != null && !pc.pickedUp)
        {
            pc.pickedUp = true;
            peanuts += pc.worth;
            indicator.text = "Peanuts: " + peanuts;
            Destroy(pc.gameObject);
        }
    }
}
