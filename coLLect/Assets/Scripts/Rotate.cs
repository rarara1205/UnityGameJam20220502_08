using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotateVector;
    //public bool rbRotation = false;

    //private Rigidbody rb;

    //private void Start()
    //{
    //    if (rbRotation) {
    //        rb = GetComponent<Rigidbody>();
    //        rb.angularVelocity = rotateVector;
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        //if (!rbRotation)
        //{
            transform.Rotate(rotateVector);
        //}
    }
}
