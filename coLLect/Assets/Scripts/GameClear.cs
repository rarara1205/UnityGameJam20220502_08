using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear: MonoBehaviour
{
    public AudioClip gameClearClip;

    private float timer;
    private bool compFadeOut;
    private Image image;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        defaultColor = image.color;
        Debug.Log(defaultColor);
        image.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0);
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
                image.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 1);
                GManager.instance.PlaySE(gameClearClip);
                naichilab.RankingLoader.Instance.SendScoreAndShowRanking(GManager.instance.time);
                compFadeOut = true;
            }

            image.color = new Color(defaultColor.r, defaultColor.g, defaultColor.b, timer);
            timer += Time.deltaTime;
        }


    }
}
