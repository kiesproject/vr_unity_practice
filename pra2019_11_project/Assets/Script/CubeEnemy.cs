using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : MonoBehaviour
{
    public float Move = 0.05f;　//敵（キューブ）の速度
    public int score = 1; //敵のポイント

    public GameObject Player;
    public GameObject GameManager;

    public Rigidbody rb;
    
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

        rb.velocity = transform.forward * Move;
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
