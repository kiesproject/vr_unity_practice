using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    //Listで消した壁の縦軸と横軸の数値を入れる
    public static List<int> vertical = new List<int>();
    public static List<int> horizontal = new List<int>();

    void Start()
    {
        //3秒後Appear関数を実行する
        Invoke("Appear", 3);
    }

    //MazeGeneratorとMinerから受け取った引数をListに追加する
    public void Receive(int ver, int hor)
    {
        vertical.Add(ver);
        horizontal.Add(hor);
    }

    private void Appear()
    {

        //2つのListの要素数（入れたデータの数）を呼ぶ
        int verCount = vertical.Count;

        //ランダム要素で選ぶ
        int random = Random.Range(1, verCount);

        //このオブジェクトを消した壁のランダムで選んだ位置に移動
        transform.position = new Vector3(horizontal[random], 0, vertical[random]);
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Player"))
        {
            Destroy(gameObject);
        }

    }

}