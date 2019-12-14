using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public float MAX_TIME = 5;
    float timeCount;

    // Use this for initialization
    void Start()
    {
        timeCount = MAX_TIME;
        GetComponent<Text>().text = ((int)timeCount).ToString();

        //*** ============================================================================================
        //*** [アドバイス]Text型の変数を宣言して格納しておくといちいちGetComponentしなくても良くなります。
        //***             GetComponentはそこそこ重い命令なのでUpdate内に書くのは避けましょう。
        //*** ============================================================================================
    }

    // Update is called once per frame
    void Update()
    {
        timeCount -= Time.deltaTime;
        GetComponent<Text>().text = ((int)timeCount).ToString();
        if (timeCount <= 1)
        {
            GetComponent<Text>().text = "スタート！！";
        }
    }
}
