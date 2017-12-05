using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    const byte BMAX = byte.MaxValue;

    public RectTransform mainMenu;
    public RectTransform areYouSure;
    public RectTransform credits;
    public RectTransform loadScreen;
    public AudioClip buttonClick;

    private RectTransform current;
    
    private void RestoreColor(Image i)
    {
        ChangeColor(i, BMAX, BMAX, BMAX, BMAX);
    }
    private void ChangeColor(Image i, byte r, byte g, byte b, byte a)
    {
        i.color = new Color32(r, g, b, a);
    }

    private void Show(RectTransform t) { t.gameObject.SetActive(true); current = t; }
    private void Hide(RectTransform t) { t.gameObject.SetActive(false); }
    private void HideCurrent() { Hide(current); }

    private void Start()
    {
        Show(mainMenu);
        Hide(credits);
        Hide(loadScreen);
        Hide(areYouSure);
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
        RestoreColor(mainMenu.GetComponent<Image>());
        HideCurrent();
        Show(mainMenu);
    }
    public void OnAreYouSure(int brightness)
    {
        byte b = (byte)Mathf.Clamp(brightness, 0, BMAX);
        ChangeColor(mainMenu.GetComponent<Image>(), b, b, b, BMAX);
        Show(areYouSure);
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
