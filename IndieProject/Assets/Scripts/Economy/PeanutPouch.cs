using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
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
    
    //public Powerup[] powerups;
    
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
                acornsInd.text = acorns.ToString();
                break;
            case NutType.Peanut:
                peanuts += amount;
                peanutInd.text = peanuts.ToString();
                break;
            case NutType.Seed:
                seeds += amount;
                seedsInd.text = seeds.ToString();
                break;
        }
    }
}

[System.Serializable]
public struct Powerup
{
    public string name;
    public int cost;
    public NutType nut;
    public KeyCode key;
    public float multiplier;
    public float duration;

    public Powerup(string name, int cost, NutType nut, KeyCode key, float multiplier, float duration)
    {
        this.name = name;
        this.cost = cost;
        this.nut = nut;
        this.key = key;
        this.duration = duration;
        this.multiplier = multiplier;
    }
}
