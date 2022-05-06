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
    public GameObject[] LImages;

    private GameObject p;
    private Player player;
    private bool doGameOver = false;
    private bool doGameClear = false;
    private float timer = 0f;

    void Awake()
    {
        gameOverObj.SetActive(false);
        gameClearObj.SetActive(false);
        for (int i = 0; i < LImages.Length; i++) LImages[i].SetActive(false);
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

        if (GManager.instance.isAddLNum)
        {
            LImages[GManager.instance.collectedLNum - 1].SetActive(true);
            GManager.instance.isAddLNum = false;
        }

        if (GManager.instance.isWarping)
        {
            UpdateWarp();
        }
    }

    public void StartWarp(int warpStageNum)
    {
        GManager.instance.isWarping = true;
        timer = 0f;
        p.transform.position = warpPoint[warpStageNum].transform.position;
        cameraController.ChangeCamera(warpStageNum);
        if(warpStageNum != 0) cameraController.FollowPlayer(p.transform);
        GManager.instance.currentStageNum = warpStageNum;
    }

    private void UpdateWarp()
    {
        if(timer < 2f)
        {
            //p.transform.Find("Spere").Renderer.material.SetFloat("_Dissolve_Time", timer);
            //p.transform.Find("Cube").Renderer.material.SetFloat("_Dissolve_Time", timer);
        }
        else
        {
            CompleteWarp();
        }
        timer += Time.deltaTime;
    }

    private void CompleteWarp()
    {
        GManager.instance.isWarping=false;
    }
}
