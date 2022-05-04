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

    private Rigidbody rb = null;
    private Vector3 oldPos;
    private bool isGround = false;
    private bool isJumping = false;
    private float jumpPos = 0.0f;
    private float horizontalKey = 0f;
    private float verticalKey = 0f;
    private bool jumpKey = false;
    private float xSpeed = 0.0f;
    private float zSpeed = 0.0f;

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
        Vector3 keyVector = new Vector3(horizontalKey, 0, verticalKey).normalized;
        xSpeed = keyVector.x * moveSpeed;
        zSpeed = keyVector.z * moveSpeed;
    }

    void FixedUpdate()
    {
        float ySpeed = -gravity;

        isGround = ground.IsGround();

        if (isGround)
        {
            if (jumpKey)
            {
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;

                isJumping = true;
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
            }
            else
            {
                isJumping = false;
            }
        }

        rb.velocity = new Vector3(xSpeed, ySpeed, zSpeed);

        Vector3 diff = transform.position - oldPos;
        diff.y = 0f;
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff);
        }
        oldPos = transform.position;
    }
}
