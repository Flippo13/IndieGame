using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class WinLoseScreen : MonoBehaviour {

    public GameObject winScreen;
    public GameObject loseScreen; 

	// Use this for initialization
	void Start () {
        winScreen.SetActive(false);
        loseScreen.SetActive(false); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Win()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true); 
    }

    public void Lose()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true); 
    }
}
