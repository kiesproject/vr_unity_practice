using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    //上から見て縦、Z軸のオブジェクトの量
    public int vertical = 15;
    //上から見て縦、X軸のオブジェクトの量
    public int horizontal = 15;

    //Prefabを入れる欄を作る
    public GameObject cube;

    //for文でオブジェクトを縦横に並べるための変数
    int vi;
    int hi;

    //MinerのPrefabを入れるための変数
    public GameObject miner;

    void Start()
    {
        //Cubeを並べるための基準になる位置
        Vector3 pos = new Vector3(0, 0, 0);

        //Z軸にverticalの数だけ並べる
        for(vi = 0; vi < horizontal; vi++)
        {
            //X軸にhorizontalの数だけ並べる
            for(hi = 0; hi < horizontal; hi++)
            {
                //PrefabのCubeを生成する
                GameObject copy = Instantiate(cube,
                //生成したものを配置する位置
                new Vector3(pos.x + hi, pos.y, pos.z + vi),
                //Quaternion.identityは無回転を指定する
                Quaternion.identity);
                //生成したオブジェクトに番号の名前をつける
                copy.name = vi + "-" + hi.ToString();
            }

        }

        //ランダムな数字を縦横分の2つ出す
        //0からだが、並ぶオブジェクトの内側から選びたいので1からにした
        int ver1 = Random.Range(1, vertical - 1);
        int hor1 = Random.Range(1, horizontal - 1);

        //ランダムな数字からオブジェクトを検索してDestroyで消す
        GameObject start = GameObject.Find(ver1 + "-" + hor1);
        Destroy(start);

        //Minerを生成
        GameObject minerObj = Instantiate(miner, Vector3.zero, Quaternion.identity);
        //Minerオブジェクトのminerスクリプトを取得
        Miner minerScr = minerObj.GetComponent<Miner>();
        //MinerスクリプトのMining関数に引数を送って実行させる
        minerScr.DoMining(ver1, hor1);

        //Walkerオブジェクト検索しWalkerスクリプトを取得、
        //そしてReceive関数に引数を送って実行する
        GameObject walker = GameObject.Find("Main Camera");
        Walker walkerScr = walker.GetComponent<Walker>();
        walkerScr.Receive(ver1, hor1);

        //Walkerオブジェクト検索しWalkerスクリプトを取得、
        //そしてReceive関数に引数を送って実行する
        GameObject item = GameObject.Find("Item");
        Item itemScr = item.GetComponent<Item>();
        itemScr.Receive(ver1, hor1);
    }

}