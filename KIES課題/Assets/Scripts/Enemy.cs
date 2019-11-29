using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;//追いかける対象
    public float speed = 0.04f;//追いかける速さ
    private Vector3 vct;//位置

    void Start()
    {
        transform.position = new Vector3(Random.Range(-17,17),0,Random.Range(-9,9));  //フィールド内のランダムな位置に出現させる
    }
    void Update()
    {
        //targetの方に少しずつ向きが変わる
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), 0.3f);

        //targetに向かって進む
        transform.position += transform.forward * speed;

        if (controler.instance.timer2 >= 9.955)   //Timer2が10秒たつごとにspeedを0.01ずつ上げる
        {
            speed += 0.01f;
        }
    }
}
