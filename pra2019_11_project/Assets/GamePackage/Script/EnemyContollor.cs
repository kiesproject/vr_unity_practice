using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyContollor : MonoBehaviour
{
    public Transform target;
    public float speed;
    private Vector3 vec;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //targetの方に少しずつ向きが変わる
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 0.9f);

        //targetに向かって進む
        transform.position += transform.forward * speed * 0.1f;
    }
}
