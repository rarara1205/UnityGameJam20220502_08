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
            stageController.Warp(warpStageNum);
        }
    }
}
