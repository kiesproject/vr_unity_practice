using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text score;          //表示するもの
    private GameObject player;   //プレイヤー

    void Start()
    {
        score = GetComponent<Text>();   //scoreはTextだと宣言する
    }

    
    void Update()
    {
        //====================================================================================================================
        //*** [改善] GameObject.Find()などのFind系の命令は実行時間が長いのでUpdate()内に書いてしまうと重くなる原因になります。
        //***        プレイヤーを取得する場合、Find系の命令はStartやAwakeに書くと良いでしょう。
        //====================================================================================================================


        player = GameObject.Find("Player");  //プレイヤーを見つける
        score.text = "Time:" + ((int)controler.instance.timer).ToString("00");   //タイマーにTimerの値を反映させる
        if(player == null)                //もしプレイヤーが存在しなかったのならばこのゲームオブジェクトを非表示にする
        {
            this.gameObject.SetActive(false);
        }
    }
}
