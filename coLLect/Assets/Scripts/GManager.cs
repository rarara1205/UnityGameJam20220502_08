using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    public float time;
    public int currentStageNum;
    public int collectedLNum;
    public int hpNum;
    public int defaultHpNum;
    public string playerTag = "Player";
    public string groundTag = "Ground";
    public string moveFloorTag = "MoveFloor";
    public string enemyTag = "Enemy";
    //public string fallFloorTag = "FallFloor";
    [HideInInspector] public bool isGameOver = false;
    [HideInInspector] public bool isGameClear = false;
    [HideInInspector] public bool isAddLNum = false;
    [HideInInspector] public bool isWaiting = false;
    [HideInInspector] AudioSource audioSource;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void AddLNum()
    {
        collectedLNum++;
        isAddLNum = true;
        if (collectedLNum == 3)
        {
            isGameClear = true;
        }
    }

    public void SubHpNum()
    {
        hpNum--;
        if (hpNum <= 0) isGameOver = true;
    }

    public void RetryGame()
    {
        isGameClear = false;
        isGameOver = false;
        hpNum = defaultHpNum;
        currentStageNum = 0;
        time = 0f;
        collectedLNum = 0;
    }

    public void PlaySE(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
