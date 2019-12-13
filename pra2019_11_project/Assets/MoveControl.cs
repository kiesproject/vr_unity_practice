﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public float speed = 3f;
    float moveX = 0f;
    float moveZ = 0f;
    Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //playerの制御
        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(moveX, 0, moveZ);
    }

}
