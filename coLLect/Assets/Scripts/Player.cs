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
    [HideInInspector] public bool isContinue = false;

    private Rigidbody rb = null;
    private Vector3 oldPos;
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
    private string enemyTag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {
        oldPos = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody>();
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
        if(!GManager.instance.isGameOver && !GManager.instance.isGameClear)
        {
            GetXZSpeed();
            GetYSpeed();
            rb.velocity = new Vector3(xSpeed, ySpeed, zSpeed);
            IRotate();
        }
    }

    private void GetXZSpeed()
    {
        Vector3 keyVector = new Vector3(horizontalKey, 0, verticalKey).normalized;
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
        Vector3 diff = transform.position - oldPos;
        diff.y = 0f;
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
        }
        oldPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == enemyTag)
        {
            GManager.instance.SubHpNum();
            if (!GManager.instance.isGameOver)
            {
                isContinue = true;
            }
        }
    }
}
