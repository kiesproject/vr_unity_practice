using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //目的地となるオブジェクトのトランスフォーム格納用
    public Transform target;
    //エージェントとなるオブジェクトのNavMeshAgent格納用
    public NavMeshAgent agent;

    // Use this for initialization
    void Update()
    {
        //目的地となる座標を設定する
        agent.destination = target.position;
    }
}
