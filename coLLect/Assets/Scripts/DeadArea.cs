using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    public StageController stageController;
    public PlayerTriggerCheck playerTriggerCheck;

    private void OnTriggerEnter(Collider other)
    {
        if (playerTriggerCheck.isOn)
        {
            GManager.instance.SubHpNum();
            if (!GManager.instance.isGameOver)
            {
                stageController.player.isContinue = true;
            }
        }
    }
}
