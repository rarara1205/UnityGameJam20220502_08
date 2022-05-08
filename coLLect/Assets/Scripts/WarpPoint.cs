using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoint : MonoBehaviour
{
    public StageController stageController;
    public PlayerTriggerCheck playerTriggerCheck;
    public int warpStageNum;

    void Update()
    {
        if (playerTriggerCheck.isOn)
        {
            if (!GManager.instance.isWaiting)
            {
                stageController.StartWarp(warpStageNum);
                GManager.instance.isWaiting = true;
            }
        }
    }
}
