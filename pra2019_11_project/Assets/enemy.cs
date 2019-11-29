﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform target;
    public float speed = 1.0f;
    private Vector3 vec;


    void Start()
    {
        target = GameObject.Find("対象").transform;
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 1.0f);
        transform.position += transform.forward * speed * 0.01f;
    }
}
