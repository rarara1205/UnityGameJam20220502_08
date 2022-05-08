using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver: MonoBehaviour
{
    public AudioClip gameOverClip;

    private float timer;
    private bool compFadeOut;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!compFadeOut)
        {
            if (timer > 1f)
            {
                foreach (Transform child in this.transform)
                {
                    child.gameObject.SetActive(true);
                }
                image.color = new Color(1, 1, 1, 1);
                GManager.instance.PlaySE(gameOverClip);
                compFadeOut = true;
            }

            image.color = new Color(1, 1, 1, timer);
            timer += Time.deltaTime;
        }


    }
}
