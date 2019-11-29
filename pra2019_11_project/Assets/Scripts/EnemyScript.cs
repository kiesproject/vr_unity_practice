using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript: MonoBehaviour
{

    public NavMeshAgent Enemy;
    public GameObject Player;

    void Start()
    {
        Enemy = gameObject.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Player != null)
        {
            Enemy.destination = Player.transform.position;
        }
    }
}