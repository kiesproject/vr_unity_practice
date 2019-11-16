using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blockPrefabs;

    [SerializeField, Tooltip("ブロックのサイズ")]
    private Vector3 sizeBlock = new Vector3(1, 1, 1);


    //--- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- --- ---

    //===ダンジョン===
    private int horizontal_size = 0;                    //ダンジョンのサイズ
    private int vertical_size = 0;                      //ダンジョンのサイズ
    private Vector3 baseposs_DG = Vector3.zero;         //ダンジョンの中心座標

    //===矩形===
    private int time_Ganaration_Rect = 10;
    private int x_Size_MinRect = 20;
    private int y_Size_MinRect = 20;


    private TileData[] tileDate;        //タイルデータ

    //---
    private Rect originRect;            //根の矩形データ

    private void Start()
    {
        //タイルデータを作成
        //Create_TileData(100, 100);


        /*
        Rect rect = new Rect(0, 0, 10, 10);
        rect.Create_ChildRect(1);
        rect.rects[0].Create_ChildRect(0);
        rect.rects[1].Create_ChildRect(0);
        Test_RectTree(rect, 0);


        var v = Rect.Check_pointUpRect(5, 0, rect);
        Rect.Test_Rect(v);

        Debug.Log("--- --- --- --- --- --- --- ---");
        foreach(Rect r in v.Get_AdjaRect(rect))
        {
            Rect.Test_Rect(r);
        }
        */

        Create_Labyrinth();

        //Test_RectTree(originRect,0);
    }

    //  ◇========================================================================◇
    //   ||〇　〇　タ　イ　ル　デ　ー　タ　操　作　系　の　メ　ソ　ッ　ド　〇　〇 ||
    //  ◇========================================================================◇

    private void Create_TileData(int sizeX, int sizeY)
    {
        tileDate = new TileData[sizeX * sizeY];
        horizontal_size = sizeX;
        vertical_size = sizeY;
    }

    /// <summary>
    /// 指定した座標のタイルIDを上書きする
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="id"></param>
    private void Set_TileData(int x, int y, int id)
    {
        Get_TileData(x, y).TileID = id;
    }

    /// <summary>
    /// 指定した範囲内のタイルデータを塗りつぶす
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="id"></param>
    private void Fill_TileData(int x, int y, int width, int height, int id)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Set_TileData(x + i, y + j, id);
            }
        }
    }

    //====================================================

    /// <summary>
    /// 指定されたindexのタイルデータを取得する
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private TileData Get_TileData(int index)
    {
        if (tileDate.Length != 0)
        {
            if ((0 < index) && (index < tileDate.Length))
            {
                return tileDate[index];
            }
        }
        Debug.LogError("[Labyrinyh] タイルデータの取得に失敗 : タイルデータが存在しない or 指定されたindexが不適切");
        return null;
    }

    /// <summary>
    /// 指定された座標のタイルデータを取得する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private TileData Get_TileData(int x, int y)
    {
        return Get_TileData(Get_TileIndex(x, y));
    }

    /// <summary>
    /// タイル座標をタイルデータ配列の要素数に変換する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int Get_TileIndex(int x, int y)
    {
        return y * horizontal_size + x;
    }

    private void Create_Room()
    {

    }


    //  ◇================================================================◇
    //   ||◇　◇　ブ　ロ　ッ　ク　に　関　す　る　メ　ソ　ッ　ド　◇　◇ ||
    //  ◇================================================================◇

    /// <summary>
    /// ブロック(Prefab)を設置する(立体)
    /// </summary>
    /// <param name="x">ブロックを置く座標X</param>
    /// <param name="y">ブロックを置く座標Y</param>
    /// <param name="h">ブロックを置く座標・高さ</param>
    /// <param name="id">登録してあるブロックのID</param>
    private GameObject SetBlock(int x, int y,int h, int id)
    {
        if (Chack_BlockID(id))
        {
            return Instantiate(blockPrefabs[id], baseposs_DG + new Vector3(x * sizeBlock.x, h * sizeBlock.z, y * sizeBlock.y), Quaternion.identity);
        }
        return null;
    }

    /// <summary>
    /// ブロック(Prefab)を設置する(平面)
    /// </summary>
    /// <param name="x">ブロックを置く座標X</param>
    /// <param name="y">ブロックを置く座標Y</param>
    /// <param name="h">ブロックを置く座標・高さ</param>
    /// <returns></returns>
    private GameObject SetBlock(int x, int y, int id)
    {
        return SetBlock(x, y, 0, id);
    }

    /// <summary>
    /// ブロック(Prefab)を敷き詰める
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="h"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="id"></param>
    private void FillBlock(int x, int y, int h, int width, int height, int id)
    {
        for (int i=0; i < width; i++)
        {
            for (int j=0; j < height; j++)
            {
                SetBlock(x + i ,y + j, h, id);
            }
        }
    }

    /// <summary>
    /// ブロック(Prefab)を敷き詰める
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <param name="id"></param>
    private void FillBlock(int x, int y, int width, int height, int id)
    {
        FillBlock(x, y, 0, width, height, id);
    }

    /// <summary>
    /// ブロックのIDが適切か
    /// </summary>
    /// <returns></returns>
    private bool Chack_BlockID(int id)
    {
        return (0 <= id && id <= blockPrefabs.Length - 1);
    }

    private void Put_TileTrace()
    {

    }


    //  ◇=========================================================◇
    //   ||☆　☆　迷　宮　に　関　す　る　メ　ソ　ッ　ド　☆　☆ ||
    //  ◇=========================================================◇

    /// <summary>
    /// 迷宮の生成
    /// </summary>
    void Create_Labyrinth()
    {
        int time_gene = 10;
        int x_size_laby = 100;
        int y_size_laby = 100;
        int x_size_minRect = 20;
        int y_size_minRect = 20;

        //タイルデータを生成・初期化
        Create_TileData(x_size_laby, y_size_laby);
        //矩形を自動生成
        Create_rect_Labyrinth_f(time_gene, x_size_laby, y_size_laby, x_size_minRect, y_size_minRect);
        Test_RectTree(originRect, 0);

        var list_rect_top = Get_TopRect(originRect);

        Debug.Log("");
        foreach(Rect r in list_rect_top)
        {
            Rect.Test_Rect(r);
        }
    }

    /// <summary>
    /// 矩形群を生成(幅不定)
    /// </summary>
    /// <param name="gene">Rect生成数</param>
    /// <param name="xSize_O">生成する根RectのサイズX</param>
    /// <param name="ySize_O">生成する根RectのサイズY</param>
    /// <param name="minX">生成するRectの最小面積X座標</param>
    /// <param name="minY">生成するRectの最小面積Y座標</param>
    /// <returns>実際に生成したRectの数</returns>
    private int Create_rect_Labyrinth(int gene, int xSize_O, int ySize_O, int minX, int minY)
    {
        int count = 0;
        originRect = new Rect(0, 0, xSize_O, ySize_O); //ベースとなる矩形を作成
        Rect rect = originRect;
        var rectList = new List<Rect>();
        rectList.Add(rect);

        for (int i=0; i<gene; i++)
        {
            Debug.Log(string.Format("生成: {0}\nスケール: {1}  width: {2} height: {3}", i, rect.scale, rect.width, rect.height));
            int q = 0;
            int length = -1;
            int d = Random.Range(0, 2);

            //分割作成が可能かどうかの判定
            if (rect.width < 2 * minX || rect.height < 2 * minY)
            {
                if (!(rect.width < 2 * minX))
                {
                    d = 0;
                }
                else if(!(rect.height < 2 * minY))
                {
                    d = 1;
                }
                else
                {
                    Debug.Log("\t作成不可");
                    rectList.Remove(rect);
                    if (rectList.Count == 0) continue;
                    rect = rectList[Random.Range(0, rectList.Count)];
                    continue;
                }
            }

            //length決定
            if (d == 0)
            {
                length = Random.Range(minX, rect.width - minX);
            }
            else
            {
                length = Random.Range(minY, rect.height - minY);
            }

            //テスト
            /*
            Rect testRect = new Rect(rect.x, rect.y, rect.width, rect.height);
            testRect.Create_ChildRect(length, d);
            foreach(Rect r in testRect.rects)
            {
                if (r.width < minX) Debug.LogError("\tXの最小値を下回っています");
                if (r.height < minY) Debug.LogError("\tYの最小値を下回っています");
            }
            */

            //生成したRectをリストに登録　→　次のRectを決定
            if (rectList.Count != 0)
            {
                rect.Create_ChildRect(length, d);
                count++;
                rectList.AddRange(rect.rects);
                rectList.Remove(rect);
                rect = rectList[Random.Range(0, rectList.Count)];
            }
            else
            {
                break; //作成可能のRectが無くなったので終了
            }
        }

        return count;
    }

    /// <summary>
    /// 矩形群を生成(幅均等)
    /// </summary>
    /// <param name="gene">Rect生成数</param>
    /// <param name="xSize_O">生成する根RectのサイズX</param>
    /// <param name="ySize_O">生成する根RectのサイズY</param>
    /// <param name="minX">生成するRectの最小面積X座標</param>
    /// <param name="minY">生成するRectの最小面積Y座標</param>
    /// <returns>実際に生成したRectの数</returns>
    private int Create_rect_Labyrinth_f(int gene, int xSize_O, int ySize_O, int minX, int minY)
    {
        int count = 0;
        originRect = new Rect(0, 0, xSize_O, ySize_O); //ベースとなる矩形を作成
        Rect rect = originRect;
        var rectList = new List<Rect>();
        rectList.Add(rect);

        for (int i = 0; i < gene; i++)
        {
            Debug.Log(string.Format("生成: {0}\nスケール: {1}  width: {2} height: {3}", i, rect.scale, rect.width, rect.height));
            int d = Random.Range(0, 2);

            //分割作成が可能かどうかの判定
            if (rect.width < 2 * minX || rect.height < 2 * minY)
            {
                if (!(rect.width < 2 * minX))
                {
                    d = 0;
                }
                else if (!(rect.height < 2 * minY))
                {
                    d = 1;
                }
                else
                {
                    Debug.Log("\t作成不可");
                    rectList.Remove(rect);
                    if (rectList.Count == 0) continue;
                    rect = rectList[Random.Range(0, rectList.Count)];
                    continue;
                }
            }

            //テスト
            /*
            Rect testRect = new Rect(rect.x, rect.y, rect.width, rect.height);
            testRect.Create_ChildRect(length, d);
            foreach(Rect r in testRect.rects)
            {
                if (r.width < minX) Debug.LogError("\tXの最小値を下回っています");
                if (r.height < minY) Debug.LogError("\tYの最小値を下回っています");
            }
            */

            //生成したRectをリストに登録　→　次のRectを決定
            if (rectList.Count != 0)
            {
                rect.Create_ChildRect(d);
                count++;
                rectList.AddRange(rect.rects);
                rectList.Remove(rect);
                rect = rectList[Random.Range(0, rectList.Count)];
            }
            else
            {
                break; //作成可能のRectが無くなったので終了
            }
        }

        return count;
    }



    /// <summary>
    /// 最子供Rectをリスト化
    /// </summary>
    /// <param name="origin"></param>
    /// <returns></returns>
    private List<Rect> Get_TopRect(Rect origin)
    {
        var rList = new List<Rect>();

        minerRect(origin);

        return rList;

        void minerRect(Rect rect)
        {
            //Rect.Test_Rect(rect);
            if (rect.rects.Count == 0)
            {
                //Debug.Log("追加Rect : " + rect.Test_RS());
                rList.Add(rect);
            }

            foreach(Rect r in rect.rects)
            {
                minerRect(r);
            }
        }
    }

    /// <summary>
    /// Rectの階層を表示する
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="i"></param>
    private void Test_RectTree(Rect rect, int i)
    {
        //階層の深さの設定
        i++;
        if (rect.parent == null) i = 0;
        
        //文字列宣言(リセット)
        string ts = "";

        //タブを複数追加
        for (int j=0; j<i; j++)
            ts += "----";

        //文字列出力
        ts += string.Format("〇 x: {0} y: {1} width: {2} height: {3}", rect.x, rect.y, rect.width, rect.height);
        Debug.Log(ts + ((rect.rects.Count==0) ? " ☆" : ""));

        //再帰的に実行する
        foreach (Rect r in rect.rects)
        {
            Test_RectTree(r, i);
        }

    }
}

/// <summary>
/// タイルデータ
/// </summary>
public class TileData
{
    /*
        0: 外壁
        1: 内壁
        2: 部屋
        3: 玄関
        4: 通路
    */
    public int TileID = 0;
    public int BlockID = 0;
}