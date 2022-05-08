using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LItem : MonoBehaviour
{
    public PlayerTriggerCheck playerTriggerCheck;
    public AudioClip getLItemClip;

    // Update is called once per frame
    void Update()
    {
        if (playerTriggerCheck.isOn)
        {
            GManager.instance.PlaySE(getLItemClip);
            GManager.instance.AddLNum();
            Destroy(this.gameObject);
        }
    }
}
