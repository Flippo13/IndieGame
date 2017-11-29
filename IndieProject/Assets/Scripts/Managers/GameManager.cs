using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : ManagerBase {
    
    public static GameManager instance = null;
    
    private List<ManagerBase> managers;

    [HideInInspector] public UIManager userInterface;
    [HideInInspector] public AchievementManager achievements;
    [HideInInspector] public ScoreManager score;
    [HideInInspector] public SaveManager save;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SettupLibrary();
            EnterScene();
        }
        else
        {
            instance.EnterScene();
            Destroy(gameObject);
        }
    }

    private void SettupLibrary()
    {
        managers = new List<ManagerBase>();

        userInterface = GetSingleManager<UIManager>();
        achievements = GetSingleManager<AchievementManager>();
        score = GetSingleManager<ScoreManager>();
        save = GetSingleManager<SaveManager>();

        managers.AddRange(gameObject.GetComponents<ManagerBase>());
        foreach (ManagerBase m in managers)
        {
            m.SetUp();
        }
    }

    private T GetSingleManager<T>() where T : ManagerBase
    {
        T[] components = GetComponents<T>();
        if (components.Length == 0) return null;
        for (int a = components.Length - 1; a >= 1; a--)
        {
            print("Copy of " + components[a].GetType().ToString() + " found. <Removing Copies>");
            Destroy(components[a]);
        }
        return components[0];
    }

    public override void SetUp()
    {
        gameManager = this;
    }

    public void LoadLevel(int index)
    {
        ExitScene();
        SceneManager.LoadScene(index);
    }

    public void EnterScene() { foreach (ManagerBase m in managers) m.OnSceneEnter(); }
    public void ExitScene()  { foreach (ManagerBase m in managers) m.OnSceneExit(); }

    public override void OnSceneEnter()
    {

    }

    public override void OnSceneExit()
    {

    }
}
