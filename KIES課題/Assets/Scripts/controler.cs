using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controler : MonoBehaviour
{
    public static controler instance = null;  //これがstaticだと宣言する

    public float timer;          //ゲーム全体のタイマー（スコア）
    public float timer2;        //エネミー管理用のタイマー
    public GameObject enemy;   //エネミーのゲームオブジェクト
    public bool clear = false;  //クリア条件を満たしているのかの判断
    public GameObject hint;  //ヒント
    public GameObject item;  //アイテム
    private GameObject player;  //プレイヤー
    


    private void Awake()　　//このオブジェクトが1つしか存在しないようにするもし2つ以上存在する場合にはこのゲームオブジェクトを消去する
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void Update()
    {
        player = GameObject.Find("Player");  //プレイヤーを見つける
        if (player != null)                 //もしプレイヤーが存在するのならばTimerとTimer2の数字を上昇させる
        {
            timer += Time.deltaTime;
            timer2 += Time.deltaTime;
            if (timer2 >= 10)           //もしTimer2が10を超えたのならばTimer2をリセットしてエネミーを1体増やす
            {
                Instantiate(enemy);
                timer2 = 0;
            }
        }



        if(timer >= 55)　　　//55秒経過したらゴールの位置のヒントを出す
        {
            hint.SetActive(true);
        }
        else if(timer >= 50)   //50秒経過したらクリア可能にする
        {
            clear = true;
        }
        else if (timer >= 35)　　　　　//35秒経過したらItemをアクティブにする
        {
            item.SetActive(true);
        }
    }
}
