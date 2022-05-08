using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SManager : MonoBehaviour
{
    public FadeImage fade;

    private bool firstPush = false;
    private bool pressPlay = false;
    private bool pressRetry = false;
    private bool pressTitle = false;

    public void PressPlay()
    {
        if (!firstPush)
        {
            fade.StartFadeOut();
            firstPush = true;
            pressPlay = true;
        }
    }

    public void PressRetry()
    {
        if (!firstPush)
        {
            fade.StartFadeOut();
            firstPush = true;
            pressRetry = true;
        }
    }

    public void PressTitle()
    {
        if (!firstPush)
        {
            fade.StartFadeOut();
            firstPush= true;
            pressTitle = true;
        }

    }

    private void Update()
    {
        if (fade.IsFadeOutComplete() && pressPlay)
        {
            Debug.Log("fadeoutcomp");
            SceneManager.LoadScene("Game");
            pressPlay = false;
        }

        if (fade.IsFadeOutComplete() && pressRetry)
        {
            SceneManager.LoadScene("Game");
            GManager.instance.RetryGame();
            pressRetry = false;
        }

        if (fade.IsFadeOutComplete() && pressTitle)
        {
            SceneManager.LoadScene("Title");
            GManager.instance.RetryGame();
            pressTitle = false;
        }
    }
}
