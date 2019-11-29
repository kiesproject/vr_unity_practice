using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//今回のゲームには関係なし
public class WallTest1 : MonoBehaviour
{
    //オブジェクトを宣言
    public GameObject wallPrefab;

    //ランダム數値の制禦
    private int randomMin = 0;
    private int randomMax = 2;

    //配列の幅
    public int m_width = 10; //x軸方向
    public int m_heigt = 10; //y軸方向



    // Use this for initialization
    void Start()
    {

        //２次元の配列にする
        int[,] map = new int[m_width, m_heigt];

        //for文を用ゐて各インデックスに1もしくは0を代入
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = 1;//Random.Range(randomMin, randomMax);
            }
        }

        //各インデックスに代入された値を基に、壁の生成、不生成を判別
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                //インデックスの値が1の時、wallPrefabを生成
                if (map[i, j] == 1)
                {
                    GameObject go = Instantiate(wallPrefab);
                    go.transform.position = new Vector3(i, 0, j);

                }
            }
        }
    }
}
