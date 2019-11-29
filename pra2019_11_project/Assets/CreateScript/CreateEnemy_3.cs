﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateEnemy_3 : MonoBehaviour
{

    public GameObject originalObject;
    public float timeOut;
    public float Nowtime;
    readonly TimeManager fin_time;

    // Use this for initialization
    void Start()
    {
        Nowtime = 9.0f;
    }

    // Update is called once per frame
    //enemyを右下隅から一定時間ごとに生成する
    void Update()
    {
        if (fin_time == false)
        {
            Nowtime += Time.deltaTime;
            if (Nowtime >= timeOut)
                Nowtime += Time.deltaTime;
            if (Nowtime >= timeOut)
            {
                GameObject enemy = Instantiate(originalObject) as GameObject;
                enemy.transform.position = new Vector3(10.0f, 0.5f, -10.0f);
                //item = Instantiate(originalObject) as GameObject;
                //item.transform.position = new Vector3(-10.0f, 0.5f, 10.0f);
                Nowtime = 0.0f;
            }
        }
    }
}

