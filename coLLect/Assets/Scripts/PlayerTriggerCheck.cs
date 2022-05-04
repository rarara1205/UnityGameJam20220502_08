using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerCheck : MonoBehaviour
{
    [HideInInspector] public bool isOn = false;
    private string playerTag = "Player";

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == playerTag)
        {
            isOn = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.tag == playerTag)
        {
            isOn = false;
        }
    }
}
