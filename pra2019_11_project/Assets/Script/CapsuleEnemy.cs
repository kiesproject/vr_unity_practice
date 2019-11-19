using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleEnemy : MonoBehaviour
{
    public float Move = 0.05f;　//敵（カプセル）の速度
    public GameObject Player;
    public GameObject GameManager;
    public int score = 1;　//敵のポイント

    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトの参照を渡す
        Player = GameObject.Find("Player") as GameObject;
        GameManager = GameObject.Find("GameManager") as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //プレイヤーの方に近づく処理
        transform.LookAt(Player.transform);

        gameObject.transform.Translate(0, 0, Move);
    }

    //玉に衝突した時の処理
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "bullet")
        {
            //敵を消滅させる
            Destroy(gameObject);

            //スコアに加算する
            var sc = GameManager.GetComponent<GameManager>();
            sc.score += score;
        }
    }
}
