using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public bool firstFadeInComplete;

    private Image fadeImage = null;
    private int frameCount = 0;
    private float timer = 0.0f;
    private bool fadeIn = false;
    private bool fadeOut = false;
    private bool compFadeIn = false;
    private bool compFadeOut = false;


    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        if (firstFadeInComplete) FadeInComplete();
        else StartFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if(frameCount > 2)
        {
            if (fadeIn) FadeInUpdate();
            else if (fadeOut) FadeOutUpdate();
        }
        frameCount++;
    }


    public void StartFadeIn()
    {
        if(fadeIn || fadeOut)
        {
            return;
        }

        fadeIn = true;
        compFadeIn = false;
        timer = 0.0f;
        fadeImage.color = new Color(1, 1, 1, 1);
        fadeImage.fillOrigin = (int)Image.OriginHorizontal.Right;
        fadeImage.fillAmount = 1;
        fadeImage.raycastTarget = true;
    }

    public bool IsFadeInComplete()
    {
        return compFadeIn;
    }

    public void StartFadeOut()
    {
        if(fadeIn || fadeOut)
        {
            return;
        }

        fadeOut = true;
        compFadeOut = false;
        timer = 0.0f;
        fadeImage.color = new Color(1, 1, 1, 0);
        fadeImage.fillOrigin = (int)Image.OriginHorizontal.Left;
        fadeImage.fillAmount = 0;
        fadeImage.raycastTarget = true;
    }

    public bool IsFadeOutComplete()
    {
        return compFadeOut;
    }


    private void FadeInUpdate()
    {
        if(timer < 1f)
        {
            fadeImage.color = new Color(1, 1, 1, 1);
            fadeImage.fillAmount = 1 - timer;
        }
        else
        {
            FadeInComplete();
        }
        timer += Time.deltaTime;
    }

    private void FadeOutUpdate()
    {
        if (timer < 1f)
        {
            fadeImage.color = new Color(1, 1, 1, 1);
            fadeImage.fillAmount = timer;
        }
        else
        {
            FadeOutComplete();
        }
        timer += Time.deltaTime;
    }

    private void FadeInComplete()
    {
        fadeImage.color = new Color(1, 1, 1, 0);
        fadeImage.fillAmount = 0;
        fadeImage.raycastTarget = false;
        timer = 0f;
        fadeIn = false;
        compFadeIn = true;
    }

    private void FadeOutComplete()
    {
        fadeImage.color = new Color(1, 1, 1, 1);
        fadeImage.fillAmount = 1;
        fadeImage.raycastTarget = false;
        timer = 0f;
        fadeOut = false;
        compFadeOut = true;
    }
}
