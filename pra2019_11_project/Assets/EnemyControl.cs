using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl: MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public Transform target;
    public float speed;
    private Vector3 vec;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        //playerを追跡させる
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 0.3f);
        transform.position += transform.forward * speed;
        
    }
}
