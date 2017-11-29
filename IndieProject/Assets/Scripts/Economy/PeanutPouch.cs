using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PeanutPouch : MonoBehaviour
{
    [HideInInspector] public int peanuts;
    [HideInInspector] public int acorns;
    [HideInInspector] public int sunflowerSeeds;

    public string uiPeanuts;
    public string uiAcorns;
    public string uiSeeds;

    private void Start()
    {
        UIManager.GetUIElement<Text>(uiPeanuts, true).text = "Peanuts: " + peanuts;
        UIManager.GetUIElement<Text>(uiAcorns, true).text = "Acorns: " + acorns;
        UIManager.GetUIElement<Text>(uiSeeds, true).text = "Seeds: " + sunflowerSeeds;
    }

    private void OnTriggerEnter(Collider other)
    {
        PeanutCollectable pc = other.GetComponent<PeanutCollectable>();
        if(pc != null && pc.attractor == null)
        {
            AudioSource.PlayClipAtPoint(pc.collectSound, pc.transform.position);
            pc.Collect(gameObject);
            AddCollectible(pc.nut, pc.worth);
        }
    }

    void AddCollectible(NutType type, int amount)
    {
        switch (type)
        {
            case NutType.Peanut:
                peanuts += amount;
                UIManager.GetUIElement<Text>(uiPeanuts, true).text = "Peanuts: " + peanuts;
                break;
            case NutType.Acorn:
                acorns += amount;
                UIManager.GetUIElement<Text>(uiAcorns, true).text = "Acorns: " + acorns;
                break;
            case NutType.SunflowerSeed:
                sunflowerSeeds += amount;
                UIManager.GetUIElement<Text>(uiSeeds, true).text = "Seeds: " + sunflowerSeeds;
                break;
        }
    }
}
