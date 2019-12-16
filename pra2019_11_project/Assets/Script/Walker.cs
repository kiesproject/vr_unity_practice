using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : MonoBehaviour
{
    //Listで消した壁の縦軸と横軸の数値を入れる
    public List<int> vertical = new List<int>();
    public List<int> horizontal = new List<int>();

    //Trueになると移動できるようになる
    bool moveOn = false;

    public float speed = 2;

    void Start()
    {
        //3秒後Move関数を実行する
        Invoke("Move", 3);

        //Rigidbodyを取得し重力を消す
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if(moveOn)
        {
            //マウスの横移動によりY軸が回転して辺りを見渡せる
            transform.eulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * speed, 0);

            //WASDと上下左右キーで移動できるようにする
            float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
            float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
            transform.position += transform.forward * z + transform.right * x;
        }

    }

    //MazeGeneratorとMinerから受け取った引数をListに追加する
    public void Receive(int ver, int hor)
    {
        vertical.Add(ver);
        horizontal.Add(hor);
    }

    private void Move()
    {
        //Rigitbodyを取得し重力を付加する
        Rigidbody rb = this.GetComponent<Rigidbody>();
        rb.useGravity = true;

        //このオブジェクトを前向きにする
        transform.eulerAngles = new Vector3(0, 0, 0);

        //2つのListの要素数（入れたデータの数）を呼ぶ
        int verCount = vertical.Count;

        //ランダム要素で選ぶ
        int random = Random.Range(1, verCount);

        //*** ===============================================================
        //*** ↑要素数をランダムで選ぶならRandom.Range(0, verCount);ですね。
        //*** ===============================================================

        //このオブジェクトを消した壁のランダムで選んだ位置に移動
        transform.position = new Vector3(horizontal[random], 0, vertical[random]);

        //移動できるようにする
        moveOn = true;
    }

}