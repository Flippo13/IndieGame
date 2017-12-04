using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public RectTransform mainMenu;
    public RectTransform options;
    public RectTransform highScore;
    public RectTransform credits;
    public RectTransform loadScreen;
    public AudioClip buttonClick;

    private RectTransform current;
    
    private void Show(RectTransform t) { t.gameObject.SetActive(true); current = t; }
    private void Hide(RectTransform t) { t.gameObject.SetActive(false); }
    private void HideCurrent() { Hide(current); }

    private void Start()
    {
        Show(mainMenu);
        Hide(options);
        Hide(highScore);
        Hide(credits);
        Hide(loadScreen);
    }

    public void PlayClick()
    {
        AudioSource.PlayClipAtPoint(buttonClick, Vector3.zero);
    }

    public void OnStart()
    {
        Show(loadScreen);
        SceneManager.LoadScene(1);
    }
    public void OnBack()
    {
        HideCurrent();
        Show(mainMenu);
    }
    public void OnOptions()
    {
        HideCurrent();
        Show(options);
    }
    public void OnHighscore()
    {
        HideCurrent();
        Show(highScore);
    }
    public void OnCredits()
    {
        HideCurrent();
        Show(credits);
    }
    public void OnExit()
    {
        Application.Quit();
    }
}
