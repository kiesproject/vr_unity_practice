using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CreateEnemy : MonoBehaviour
{

    //*** ======================================================================================
    //*** [改善]敵の生成を行うためのプログラムなら１つのscriptにまとめた方がいいと思いです。
    //***       いくつかのscriptに分けると制御が大変になってしまいます。
    //*** ======================================================================================

    public GameObject originalObject;
    public float timeOut = 5.0f;
    public float Nowtime;
    readonly TimeManager fin_time;

    // Use this for initialization
    void Start()
    {
        Nowtime = 9.0f;
    }

    // Update is called once per frame
    //enemyを左上隅から一定時間ごとに生成する
    void Update()
    {
        if (fin_time == false)
        {
            Nowtime += Time.deltaTime;

            //*** =========================================
            //*** ↓
            if (Nowtime >= timeOut)
                Nowtime += Time.deltaTime;
            //*** ↑ この処理は多分要らないと思います。
            //*** =========================================

            if (Nowtime >= timeOut)
            {
                GameObject enemy = Instantiate(originalObject) as GameObject;
                enemy.transform.position = new Vector3(-10.0f, 0.5f, 10.0f);
                Nowtime = 0.0f;
            }
        }
    }
}
