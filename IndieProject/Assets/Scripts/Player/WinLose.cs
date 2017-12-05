using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : UIBase {

    public bool DebugMode;
    
    public RectTransform winScreen;
    public RectTransform loseScreen;
    public RectTransform areYouSureWin;
    public RectTransform areYouSureLose;

    private void Start()
    {
        Hide(loadingScreen);
        Hide(winScreen);
        Hide(loseScreen);
    }

    public void OpenWinScreen()
    {
        Show(winScreen);
    }

    public void OpenLoseScreen()
    {
        Show(loseScreen);
    }

    public void NextLevel()
    {
        Show(loadingScreen);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadScene()
    {
        Show(loadingScreen);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        Show(loadingScreen);
        SceneManager.LoadScene(0);
    }

    public void OnAreYouSureWin(int brightness)
    {
        byte b = (byte)Mathf.Clamp(brightness, 0, BMAX);
        ChangeColor(current.GetComponent<Image>(), b, b, b, BMAX);
        Show(areYouSureWin);
    }

    public void OnAreYouSureLose(int brightness)
    {
        byte b = (byte)Mathf.Clamp(brightness, 0, BMAX);
        ChangeColor(current.GetComponent<Image>(), b, b, b, BMAX);
        Show(areYouSureLose);
    }

    void Update () {
        if (DebugMode)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) OpenWinScreen();
            if (Input.GetKeyDown(KeyCode.Alpha2)) OpenLoseScreen();
        }
	}
}
