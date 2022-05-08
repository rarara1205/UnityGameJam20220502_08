using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGround = false;
    private bool isGroundEnter, isGroundStay, isGroundExit;
    private MoveObject moveObj = null;
    [HideInInspector] public Vector3 addVelocity;

    public bool IsGround()
    {
        if(isGroundExit)
        {
            isGround = false;
        }
        else if (isGroundEnter || isGroundStay)
        {
            isGround = true;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;

    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == GManager.instance.groundTag)
        {
            isGroundEnter = true;
        }
        else if(collision.tag == GManager.instance.moveFloorTag)
        {
            isGroundEnter = true;
            moveObj = collision.gameObject.GetComponent<MoveObject>();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == GManager.instance.groundTag)
        {
            isGroundStay = true;
        }
        else if(collision.tag == GManager.instance.moveFloorTag)
        {
            addVelocity = Vector3.zero;
            addVelocity = moveObj.GetVelocity();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == GManager.instance.groundTag)
        {
            isGroundExit = true;
        }
        else if(collision.tag == GManager.instance.moveFloorTag)
        {
            addVelocity = Vector3.zero;
            isGroundExit = true;
            moveObj = null;
        }
    }
}
