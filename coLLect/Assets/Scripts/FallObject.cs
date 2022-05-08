using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    public float fallSpeed = 10f;
    public float fallTime = 1f;
    public float vibrationWidth = 0.05f;
    public float vibrationSpeed = 30.0f;
    public GameObject meshObj;
    public float returnTime = 5f;


    private Rigidbody rb;
    private bool isOn = false;
    private bool isFalling = false;
    private bool isStanding = false;
    private Vector3 floorDefaultPos = Vector3.zero;
    private Vector3 fallVelocity = Vector3.zero;
    private float timer = 0f;
    private float fallingTimer = 0f;
    private Vector3 meshDefaultPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fallVelocity = Vector3.down * fallSpeed;
        meshDefaultPos = meshObj.transform.position;
        floorDefaultPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isOn)
        {
            isStanding = true;
            isOn = false;
        }

        if(isStanding && !isFalling)
        {
            meshObj.transform.position = meshDefaultPos + new Vector3(Mathf.Sin(timer * vibrationSpeed) * vibrationWidth, 0, 0);
            if (timer > fallTime)
            {
                isFalling = true;
            }
            timer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if(isFalling)
        {
            Debug.Log("fall");
            rb.velocity = fallVelocity;

            if(fallingTimer > returnTime)
            {
                rb.MovePosition(floorDefaultPos);
                rb.velocity = Vector3.zero;
                isFalling = false;
                timer = 0f;
                fallingTimer = 0f;
            }
            else
            {
                fallingTimer += Time.deltaTime;
                isStanding = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == GManager.instance.playerTag)
        {
            isOn = true;
        }
    }
}
