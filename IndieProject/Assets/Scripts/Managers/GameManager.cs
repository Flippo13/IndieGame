using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : ManagerBase {
    
    public static GameManager instance = null;

    private List<ManagerBase> managers;

    [HideInInspector]
    public UIManager userInterface;
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        managers = new List<ManagerBase>(gameObject.GetComponents<ManagerBase>());
        foreach (ManagerBase manager in managers)
        {
            manager.SetUp(this);
        }
    }

    public override void SetUp(GameManager pGameManager)
    {
        base.SetUp(pGameManager);
    }
}
