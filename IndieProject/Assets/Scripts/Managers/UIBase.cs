using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public abstract class UIBase : MonoBehaviour
{
    protected const byte BMAX = byte.MaxValue;
    
    protected RectTransform current = null;
    public AudioClip buttonClick;
    public RectTransform areYouSure;
    public RectTransform loadingScreen;

    protected void RestoreColor(Image i)
    {
        ChangeColor(i, BMAX, BMAX, BMAX, BMAX);
    }
    protected void ChangeColor(Image i, byte r, byte g, byte b, byte a)
    {
        if (i == null) return;
        i.color = new Color32(r, g, b, a);
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
    protected void Toggle(RectTransform t) { t.gameObject.SetActive(!gameObject.activeSelf); if (gameObject.activeSelf) current = t; }
    protected void Show(RectTransform t) { t.gameObject.SetActive(true); current = t; }
    protected void Hide(RectTransform t) { t.gameObject.SetActive(false); }
    protected void HideCurrent() { Hide(current); }


    public void QuitToMenu()
    {
        Show(loadingScreen);
        SceneManager.LoadScene(0);
    }
    public void PlayClick()
    {
        AudioSource.PlayClipAtPoint(buttonClick, Vector3.zero);
    }
    public virtual void OnAreYouSure(int brightness)
    {
        byte b = (byte)Mathf.Clamp(brightness, 0, BMAX);
        ChangeColor(current.GetComponent<Image>(), b, b, b, BMAX);
        Show(areYouSure);
    }

    public virtual void OnBack(RectTransform prev)
    {
        RestoreColor(prev.GetComponent<Image>());
        HideCurrent();
        Show(prev);
    }
}
