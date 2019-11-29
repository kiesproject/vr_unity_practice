﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{
    GameObject Run;
    GameObject Track;
    public float Speed;
   
    void Start()
    {
        Run = GameObject.Find("Runaway");
        Track = GameObject.Find("Tracker");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(Run.transform.position.x, Run.transform.position.y, Run.transform.position.z), 2.0f * Time.deltaTime);   
    }
}
