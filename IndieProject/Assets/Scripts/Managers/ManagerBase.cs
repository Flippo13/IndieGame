using UnityEngine;
using System.Collections;

public abstract class ManagerBase : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    
    public virtual void SetUp(GameManager pGameManager)
    {
        gameManager = pGameManager;
    }
}
