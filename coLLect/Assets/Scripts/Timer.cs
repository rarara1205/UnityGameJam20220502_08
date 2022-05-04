using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private void Start()
    {
        GManager.instance.time = 0f;
    }

    void Update()
    {
        if(!GManager.instance.isGameClear && !GManager.instance.isGameOver)
        {
            GManager.instance.time += Time.deltaTime;
        }
    }
}
