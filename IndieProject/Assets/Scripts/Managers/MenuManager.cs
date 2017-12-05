using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : UIBase {
    
    public RectTransform mainMenu;
    public RectTransform credits;

    private void Start()
    {
        Show(mainMenu);
        Hide(credits);
        Hide(loadingScreen);
        Hide(areYouSure);
    }


    public void OnStart()
    {
        Show(loadingScreen);
        SceneManager.LoadScene(1);
    }
    public void OnCredits(int brightness)
    {
        byte b = (byte)Mathf.Clamp(brightness, 0, BMAX);
        ChangeColor(mainMenu.GetComponent<Image>(), b, b, b, BMAX);
        Show(credits);
    }
    public void OnExit()
    {
        Application.Quit();
    }
}
