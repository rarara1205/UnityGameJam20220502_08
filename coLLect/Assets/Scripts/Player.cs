using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed;
    public float jumpSpeed;
    public float gravity;
    public float jumpHeight;
    public GroundCheck ground;
    public AnimationCurve jumpCurve;
    public AudioClip jumpClip;
    public AudioClip downClip;
    [HideInInspector] public bool isContinue = false;

    private Rigidbody rb = null;
    private bool isGround = false;
    private bool isJumping = false;
    private float jumpPos = 0.0f;
    private float horizontalKey = 0f;
    private float verticalKey = 0f;
    private bool jumpKey = false;
    private float xSpeed = 0.0f;
    private float ySpeed = 0.0f;
    private float zSpeed = 0.0f;
    private float jumpTime;
    //private SphereCollider spCol = null;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        //spCol = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalKey = Input.GetAxis("Horizontal");
        verticalKey = Input.GetAxis("Vertical");
        jumpKey = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {
        if(!GManager.instance.isGameOver && !GManager.instance.isGameClear && !GManager.instance.isWaiting)
        {
            GetXZSpeed();
            GetYSpeed();
            rb.velocity = new Vector3(xSpeed, ySpeed, zSpeed) + ground.addVelocity;
            IRotate();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void GetXZSpeed()
    {
        Vector3 keyVector = new Vector3(horizontalKey, 0, verticalKey);
        if(keyVector.magnitude >= 1)
        {
            keyVector.Normalize();
        }
        Debug.Log(keyVector.magnitude);
        xSpeed = keyVector.x * moveSpeed;
        zSpeed = keyVector.z * moveSpeed;
    }

    private void GetYSpeed()
    {
        ySpeed = -gravity;

        isGround = ground.IsGround();

        if (isGround)
        {
            if (jumpKey)
            {
                if (!isJumping)
                {
                    GManager.instance.PlaySE(jumpClip);
                }
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;
                isJumping = true;
                jumpTime = 0f;
            }
            else
            {
                isJumping = false;
                ySpeed = 0f;
            }
        }
        else if (isJumping)
        {
            if (jumpKey && jumpPos + jumpHeight > transform.position.y)
            {
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                isJumping = false;
                jumpTime = 0f;
            }
        }

        if (isJumping)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
    }

    private void IRotate()
    {
        Vector3 diff = new Vector3(xSpeed, 0f, zSpeed);
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if((collision.collider.tag == groundTag || collision.collider.tag == moveFloorTag) && collision.contacts[0].point.y < transform.position.y - spCol.radius * 0.9f )
        //{
        //    Debug.Log("aaa");
        //    transform.position += Vector3.up * 0.1f;
        //}
        if(collision.collider.tag == GManager.instance.enemyTag)
        {
            GManager.instance.SubHpNum();
            if (!GManager.instance.isGameOver)
            {
                isContinue = true;
            }
        }
    }
}
