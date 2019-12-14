using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    //目的地となるオブジェクトのトランスフォーム格納用
    public Transform target;
    //エージェントとなるオブジェクトのNavMeshAgent格納用
    public NavMeshAgent agent;

    //*** ======================================================================================================================
    //*** [アドバイス]このscriptを付けるオブジェクトにNavMeshAgentを付けるなら
    //***             agentをprivateにしてStart()内でagent = GetComponent<NavMeshAgent>();と書けば取得できます。
    //***             処理内容は変わらないですが、inspectorは少しスッキリします。また、ドラッグアンドドロップの手間が減ります。
    //*** ======================================================================================================================

    // Use this for initialization
    void Update()
    {
        //目的地となる座標を設定する
        agent.destination = target.position;
    }
}
