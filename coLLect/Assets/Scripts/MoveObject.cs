using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public GameObject[] movePoint;
    public float speed = 1f;
    public bool doReturn = false;
    public bool doVibration = true;
    public float vibrationWidth = 0.05f;
    public float vibrationSpeed = 30.0f;
    public GameObject meshObj;
    public bool waitPlayer = false;

    private Rigidbody rb;
    private bool isOn = false;
    private int nowPoint = 0;
    private bool endPoint = false;
    private Vector3 oldPos = Vector3.zero;
    private Vector3 myVelocity = Vector3.zero;
    private float timer = 0f;
    private bool isWaiting = true;
    private Vector3 defaultPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (doVibration)
        {
            defaultPos = meshObj.transform.position;
        }
        if(movePoint.Length > 0)
        {
            rb.position = movePoint[0].transform.position;
            oldPos = rb.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isOn && isWaiting && waitPlayer)
        {
            meshObj.transform.position += new Vector3(Mathf.Sin(timer * vibrationSpeed) * vibrationWidth, 0, 0);
            if (timer > 1f)
            {
                meshObj.transform.position = defaultPos;
                isWaiting = false;
            }
            timer += Time.deltaTime;
        }

        if (movePoint.Length > 1 && ((!isWaiting && isOn) || !waitPlayer))
        {
            if (!endPoint)
            {
                int nextPoint = nowPoint + 1;

                if (Vector3.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    Vector3 toVector = Vector3.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                    rb.MovePosition(toVector);
                }
                else
                {
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    ++nowPoint;
                    if (nowPoint >= movePoint.Length - 1) endPoint = true;
                }
            }
            else if (endPoint && doReturn)
            {
                int nextPoint = nowPoint - 1;

                if (Vector3.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
                {
                    Vector3 toVector = Vector3.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
                    rb.MovePosition(toVector);
                }
                else
                {
                    rb.MovePosition(movePoint[nextPoint].transform.position);
                    --nowPoint;
                    if (nowPoint <= 0)
                    {
                        endPoint = false;
                    }
                }
            }
            myVelocity = (rb.position - oldPos) / Time.deltaTime;
            oldPos = rb.position;
        }
    }

    public Vector3 GetVelocity()
    {
        return myVelocity;
    }

    public void Initialize()
    {
        isOn = false;
        isWaiting = true;
        nowPoint = 0;
        endPoint = false;
        timer = 0f;
        rb.position = movePoint[0].transform.position;
        oldPos = rb.position;
        myVelocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == GManager.instance.playerTag)
        {
            isOn = true;
        }
    }
}
