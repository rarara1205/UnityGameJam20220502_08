using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject[] warpPoint;
    public CameraController cameraController;
    public GameObject gameOverObj;
    public GameObject gameClearObj;

    private GameObject p;
    private Player player;
    private bool doGameOver = false;
    private bool doGameClear = false;

    void Awake()
    {
        gameOverObj.SetActive(false);
        gameClearObj.SetActive(false);
        p = Instantiate(playerObj, warpPoint[0].transform.position, Quaternion.identity);
        player = p.GetComponent<Player>();
        GManager.instance.currentStageNum = 0;
    }

    void Update()
    {
        if (GManager.instance.isGameOver && !doGameOver)
        {
            gameOverObj.SetActive(true);
            doGameOver = true;
        }
        else if (GManager.instance.isGameClear && !doGameClear)
        {
            gameClearObj.SetActive(true);
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking(GManager.instance.time);
            doGameClear = true;
        }
        else if (player.isContinue)
        {
            p.transform.position = warpPoint[GManager.instance.currentStageNum].transform.position;
            player.isContinue = false;
        }
    }

    public void Warp(int warpStageNum)
    {
        p.transform.position = warpPoint[warpStageNum].transform.position;
        GManager.instance.currentStageNum = warpStageNum;
        cameraController.ChangeCamera(warpStageNum);
        if(warpStageNum != 0) cameraController.FollowPlayer(p.transform);
    }
}
