using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public bool firstFadeInComplete = false;
    public bool fadeInAll = false;
    public bool fadeOutAll = false;
    public GameObject BGM;
    public float audioVolumeMax = 1f;
    public bool titleFade = false;
    public float fadeTime = 1f;

    private AudioSource audioSource;
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
        audioSource = BGM.GetComponent<AudioSource>();
        GManager.instance.isWaiting = true;
        fadeImage = GetComponent<Image>();
        if (firstFadeInComplete) FadeInComplete();
        else StartFadeIn();
    }

    // Update is called once per frame
    void Update()
    {
        if (!titleFade)
        {
            if (frameCount > 2)
            {
                if (fadeIn) FadeInUpdate();
                else if (fadeOut) FadeOutUpdate();
            }
            frameCount++;
        }
        else
        {
            if(fadeIn) FadeInUpdate();
            else if (fadeOut) FadeOutUpdate();
        }
    }


    public void StartFadeIn()
    {
        if(fadeIn || fadeOut)
        {
            return;
        }

        audioSource.volume = 0f;
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

        audioSource.volume = audioVolumeMax;
        fadeOut = true;
        compFadeOut = false;
        timer = 0.0f;
        fadeImage.color = new Color(1, 1, 1, 0);
        fadeImage.fillOrigin = (int)Image.OriginHorizontal.Left;
        if(fadeOutAll)fadeImage.fillAmount = 1;
        else fadeImage.fillAmount = 0;
        fadeImage.raycastTarget = true;
    }

    public bool IsFadeOutComplete()
    {
        return compFadeOut;
    }


    private void FadeInUpdate()
    {
        if(timer < fadeTime)
        {
            audioSource.volume = timer/fadeTime;
            if (!fadeInAll)
            {
                fadeImage.color = new Color(1, 1, 1, 1);
                fadeImage.fillAmount = 1 - timer;
            }
            else fadeImage.color = new Color(1, 1, 1, 1 - timer);
        }
        else
        {
            FadeInComplete();
        }
        timer += Time.deltaTime;
    }

    private void FadeOutUpdate()
    {
        if (timer < fadeTime)
        {
            audioSource.volume = 1f - timer/fadeTime;
            if (!fadeOutAll)
            {
                fadeImage.color = new Color(1, 1, 1, 0);
                fadeImage.fillAmount = timer;
            }
            else fadeImage.color = new Color(1, 1, 1, timer);
        }
        else
        {
            FadeOutComplete();
        }
        timer += Time.deltaTime;
    }

    private void FadeInComplete()
    {
        audioSource.volume = 1f;
        fadeImage.color = new Color(1, 1, 1, 0);
        if(!fadeInAll) fadeImage.fillAmount = 0;
        fadeImage.raycastTarget = false;
        timer = 0f;
        fadeIn = false;
        compFadeIn = true;
        GManager.instance.isWaiting = false;
    }

    private void FadeOutComplete()
    {
        audioSource.volume = 0f;
        fadeImage.color = new Color(1, 1, 1, 1);
        if(!fadeOutAll) fadeImage.fillAmount = 1;
        fadeImage.raycastTarget = false;
        timer = 0f;
        fadeOut = false;
        compFadeOut = true;
    }
}
