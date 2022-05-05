using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjDestroy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.isGameOver || GManager.instance.isGameClear) Destroy(this.gameObject);
    }
}
