using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    //オブジェクトの速度
    public float speed = 0.005f;
    //オブジェクトの横移動の最大距離
    public float max_x = 2.0f;

    // Update is called once per frame
    void Update()
    {
        //*** ===========================================
        //*** [アドバイス]sin波を使うという手もあります。
        //*** ===========================================

        //フレーム毎speedの値分だけx軸方向に移動する
        this.gameObject.transform.Translate(speed, 0, 0);

        //Transformのxの値が一定値を超えたときに向きを反対にする
        if (this.gameObject.transform.position.x > max_x || this.gameObject.transform.position.x < (-max_x))
        {
            speed *= -1;
        }
    }
}
