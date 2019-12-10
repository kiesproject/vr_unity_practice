using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public GameObject target;
    UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start() 
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.transform.position;
    }
}
