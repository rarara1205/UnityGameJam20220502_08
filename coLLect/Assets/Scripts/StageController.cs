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
    [HideInInspector]public Player player;
    public GameObject moveFloorObj;
    public float continueTime = 2.0f;

    private GameObject p;
    private bool doGameOver = false;
    private bool doGameClear = false;
    private float timer = 0f;
    private bool warpLeave = false;
    private bool warpArrive = false;
    private Rigidbody rb;
    private MoveObject moveObject;
    private MeshRenderer mr;
    private bool isContinueWaiting = false;
    private float blinkTime = 0f;

    void Awake()
    {
        gameOverObj.SetActive(false);
        gameClearObj.SetActive(false);
        for (int i = 0; i < LImages.Length; i++) LImages[i].SetActive(false);
        p = Instantiate(playerObj, warpPoint[0].transform.position, Quaternion.identity);
        player = p.GetComponent<Player>();
        GManager.instance.currentStageNum = 0;
        rb = p.GetComponent<Rigidbody>();
        moveObject = moveFloorObj.GetComponent<MoveObject>();
        mr = p.transform.GetChild(0).GetComponent<MeshRenderer>();
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
        else if (player.isContinue || isContinueWaiting)
        {
            Continue();
        }

        if (GManager.instance.isAddLNum)
        {
            LImages[GManager.instance.collectedLNum - 1].SetActive(true);
            GManager.instance.isAddLNum = false;
        }

        if (warpLeave)
        {
            UpdateWarpLeave();
        }
        else if (warpArrive)
        {
            UpdateWarpArrive();
        }
    }

    public void StartWarp(int warpStageNum)
    {
        GManager.instance.currentStageNum = warpStageNum;
        timer = 0f;
        warpLeave = true;
    }

    public void Continue()
    {
        if (player.isContinue)
        {
            GManager.instance.PlaySE(player.downClip);
            isContinueWaiting = true;
            GManager.instance.isWaiting = true;
            rb.MovePosition(warpPoint[GManager.instance.currentStageNum].transform.position);
            moveObject.Initialize();
            player.isContinue = false;
        }

        if(blinkTime > 0.4f)
        {
            mr.enabled = true;
            blinkTime = 0f;
        }
        else if(blinkTime > 0.2f)
        {
            mr.enabled = false;
        }
        else
        {
            mr.enabled = true;
        }

        if(timer > continueTime)
        {
            isContinueWaiting = false;
            GManager.instance.isWaiting = false;
            blinkTime = 0f;
            timer = 0f;
            mr.enabled = true;
        }
        else
        {
            blinkTime += Time.deltaTime;
            timer += Time.deltaTime;
        }
    }

    private void UpdateWarpLeave()
    {
        if(timer < 2f)
        {
            for(int i = 0; i < 1; i++) p.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Dissolve_Time", timer);
        }
        else
        {
            CompleteWarpLeave(GManager.instance.currentStageNum);
        }
        timer += Time.deltaTime;
    }

    private void UpdateWarpArrive()
    {
        if (timer > 0f)
        {
            for (int i = 0; i < 1; i++) p.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Dissolve_Time", timer);
        }
        else
        {
            timer = 0f;
            CompleteWarpArrive();
        }
        timer -= Time.deltaTime;
    }

    private void CompleteWarpLeave(int warpStageNum)
    {
        for (int i = 0; i < 1; i++) p.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Dissolve_Time", 2f);
        warpLeave = false;
        rb.MovePosition(warpPoint[warpStageNum].transform.position);
        cameraController.ChangeCamera(warpStageNum);
        if (warpStageNum != 0) cameraController.FollowPlayer(p.transform);
        warpArrive = true;
    }

    private void CompleteWarpArrive()
    {
        for (int i = 0; i < 1; i++) p.transform.GetChild(i).GetComponent<Renderer>().material.SetFloat("_Dissolve_Time", 0f);
        warpArrive = false;
        GManager.instance.isWaiting = false;
    }
}
