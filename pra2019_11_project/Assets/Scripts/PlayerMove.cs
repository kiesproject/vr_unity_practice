﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    public NavMeshAgent player;
    public GameObject target;

    void Start()
    {
        //*** =============================================================================
        //*** [改善]playerのアクセス修飾子はprivateにしておくとinspectorがスッキリします。
        //*** =============================================================================

        player = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target != null)
        {
            player.destination = target.transform.position;
        }
    }
}