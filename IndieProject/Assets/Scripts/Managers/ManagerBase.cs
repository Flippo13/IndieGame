using UnityEngine;
using System.Collections;

public abstract class ManagerBase : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    

    public virtual void SetUp()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    public abstract void OnSceneEnter();
    public abstract void OnSceneExit();
}
