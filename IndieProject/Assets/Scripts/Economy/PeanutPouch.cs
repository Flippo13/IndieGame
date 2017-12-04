using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PeanutPouch : MonoBehaviour
{
    [HideInInspector]
    public int peanuts;
    [HideInInspector]
    public int acorns;
    [HideInInspector]
    public int seeds;

    public Text peanutInd;
    public Text acornsInd;
    public Text seedsInd;

    private void OnTriggerEnter(Collider other)
    {
        PeanutCollectable pc = other.GetComponent<PeanutCollectable>();
        if(pc != null && pc.follow == null)
        {
            pc.OnCollect(this);
        }
    }

    public void Add(NutType type, int amount)
    {
        switch (type)
        {
            case NutType.Acorn:
                acorns += amount;
                acornsInd.text = "Acorns: " + acorns;
                break;
            case NutType.Peanut:
                peanuts += amount;
                peanutInd.text = "Peanuts: " + peanuts;
                break;
            case NutType.Seed:
                seeds += amount;
                seedsInd.text = "Seeds: " + seeds;
                break;
        }
    }
}
