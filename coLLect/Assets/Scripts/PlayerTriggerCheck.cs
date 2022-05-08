using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == GManager.instance.playerTag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == GManager.instance.playerTag)
        {
            isOn = false;
        }
    }
}
