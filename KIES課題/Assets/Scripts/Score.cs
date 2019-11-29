using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text score;    //表示するスコア

    void Start()
    {
        score = GetComponent<Text>();    //scoreはTextだと宣言する
    }


    void Update()
    {
        score.text = "Score:" + ((int)controler.instance.timer).ToString("0");  //スコアの値にTimerの値を反映させる
    }
}
