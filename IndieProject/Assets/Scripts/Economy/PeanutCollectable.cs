using UnityEngine;
using System.Collections;

public enum NutType { Peanut, Acorn, SunflowerSeed }
public class PeanutCollectable : MonoBehaviour
{
    public NutType nut;
    public int worth;
    [HideInInspector]
    public bool pickedUp = false;
}
