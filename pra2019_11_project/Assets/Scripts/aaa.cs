using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class aaa : MonoBehaviour
{
    private Text timer;//表示するもの
    private float t = 10;


    void Start()
    {
        timer = GetComponent<Text>();  //timerはTextだと宣言する
    }


    void Update()       //残り時間の表示
    {
        t -= Time.deltaTime;
        timer.text = "             無敵　残り" + ((int)t).ToString("00") + "秒";

        if(t < 0)  //ｔが0以下になったら消す
        {
            Destroy(this.gameObject);
        }
    }
}
