using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public Image hpImage1;
    public Image hpImage2;

    private float hpDivNum;
    private int oldLifeNum;

    void Start()
    {
        GManager.instance.hpNum = GManager.instance.defaultHpNum;
        hpDivNum = 1f * 1 / GManager.instance.defaultHpNum;
        oldLifeNum = GManager.instance.defaultHpNum;
    }

    // Update is called once per frame
    void Update()
    {
        if(oldLifeNum != GManager.instance.hpNum)
        {
            hpImage2.fillAmount += hpDivNum;

            if(hpImage2.fillAmount >= 0.75f)
            {
                hpImage1.color = Color.red;
            }
            else if(hpImage2.fillAmount >= 0.5f)
            {
                hpImage1.color = Color.yellow;
            }
            else
            {
                hpImage1.color = Color.green;
            }

            oldLifeNum = GManager.instance.hpNum;
        }
    }
}
