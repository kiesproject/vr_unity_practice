using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Demon : MonoBehaviour
{
    //*** ==================================================================================================================
    //*** 課題的にはこれはいいですが、せっかくNavMesh使っているならNavMeshを使った追跡も用意しても良かったかも知れませんね
    //*** ==================================================================================================================

    public GameObject target;//追いかける対象-オブジェクトをインスペクタから登録できるように
    public float speed = 0.1f;//移動スピード
    private Vector3 vec;

    void Start()
    {
        //target = GameObject.Find("対象").transform; インスペクタから登録するのでいらない
    }

    void Update()
    {
        //targetの方に少しずつ向きが変わる
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 0.3f);

        //targetに向かって進む
        transform.position += transform.forward * speed;
    }
}
