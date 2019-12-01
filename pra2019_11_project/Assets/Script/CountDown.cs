using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public static float CountDownTime;  //カウントダウンタイム
    public Text TextCountDown;  //表示用テキストUI
    public GameObject deadLabelObject;
    public GameObject timeLabelObject;

    void Start()
    {
        CountDownTime = 63.0f;  //カウントダウン開始秒数をセット
    }
    
    void Update()
    {
        //カウントダウンを整形して表示
        TextCountDown.text = String.Format("Time:{0:00.00}", CountDownTime);
        //経過時刻を引いていく
        CountDownTime -= Time.deltaTime;

        //0.0秒以下になったらカウントダウンタイムを0.0で固定(止まったように見せる)
        if (CountDownTime <= 0.0F)
        {
            CountDownTime = 0.0F;
            deadLabelObject.SetActive(true);
            timeLabelObject.SetActive(false);
        }

    }

}