using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : ManagerBase
{
    public Canvas hud;
    private Dictionary<string, Component> addressBook = new Dictionary<string, Component>();
    
    public static T GetUIElement<T>(string Name, bool Save = false) where T : Component
    {
        if (GameManager.instance == null || GameManager.instance.UserInterface == null) return null;
        return GameManager.instance.UserInterface.GetUI<T>(Name, Save);
    }

    public T GetUI<T>(string Name, bool Save = false) where T : Component
    {
        if (addressBook.ContainsKey(Name)) return addressBook[Name].GetComponent<T>();
        GameObject obj = hud.transform.Find(Name).gameObject;
        if (obj == null) return null;
        T ui = obj.GetComponent<T>();
        if (ui != null && Save) addressBook.Add(Name, ui);
        return ui;
    }

    public override void OnSceneEnter()
    {

    }

    public override void OnSceneExit()
    {

    }
}
