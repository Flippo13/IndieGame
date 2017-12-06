using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : UIBase {

    public KeyCode pauseKey;
    public RectTransform pauseScreen;

    private bool pause = false;

    private void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            pause = !pause;
            if (pause)
            {
                Show(pauseScreen);
                Time.timeScale = 0;
                return;
            }
            else
            {
                Hide(pauseScreen);
                Time.timeScale = 1;
                return;
            }
        }
    }
}
