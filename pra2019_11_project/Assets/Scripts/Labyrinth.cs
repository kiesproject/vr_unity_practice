using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Labyrinth : MonoBehaviour
{
    private int horizontal_size = 0;
    private int vertical_size = 0;

    private TileData[] tileDate;

    private void Start()
    {
        Rect rect = new Rect(0, 0, 10, 10);
        rect.Create_ChildRect(1);
        rect.rects[0].Create_ChildRect(1);
        rect.rects[1].Create_ChildRect(0);
        //Debug.Log(rect.rects[1].);
        Test_RectTree(rect, 0);

    }

    /// <summary>
    /// 迷宮の生成
    /// </summary>
    void Create_Labyrinth()
    {
        
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
        ts += string.Format("〇 x: {0} y: {1} width: {2} height: {3}", rect.x, rect.y, rect.width, rect.height); ;
        Debug.Log(ts);

        //再帰的に実行する
        foreach (Rect r in rect.rects)
        {
            
            Test_RectTree(r, i);
        }

    }

}

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
}


sealed public class Rect
{
    public List<Rect> rects = new List<Rect>();
    public Rect parent;

    public int x = 0;
    public int y = 0;
    public int width = 1;
    public int height = 1;

    //コンストラクタ
    public Rect(int x, int y, int width, int height)
    {
        Set_Rect(x, y, width, height);
        
    }


    /// <summary>
    /// 矩形を設定する
    /// </summary>
    /// <param name="x">開始点X</param>
    /// <param name="y">開始点Y</param>
    /// <param name="width">幅</param>
    /// <param name="height">高さ</param>
    public void Set_Rect(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;
        this.width = width;
        this.height = height;
    }

    /// <summary>
    /// 自分が親矩形の中に収まっているかの判定
    /// </summary>
    /// <returns></returns>
    private bool Check_Rect()
    {
        return ((x >= 0) && (y >= 0) && (x + width <= parent.width) && (y + height <= parent.height)) ? true : false;
    }

    /// <summary>
    /// 指定した矩形が自分の中に収まっているかの判定
    /// </summary>
    /// <returns></returns>
    public bool Check_ChildRect(int _x, int _y, int _width, int _height)
    {
        return ((_x >= 0) && (_y >= 0) && (_width <= x + width) && (_height <= y + height)) ? true : false;
    }

    /// <summary>
    /// 矩形の中に矩形を生成する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    public void Create_ChildRect(int x, int y, int width, int height)
    {
        if (Check_ChildRect(x, y, width, height))
        {
            Rect rect = new Rect(x, y, width, height);
            rect.parent = this;
            rects.Add(rect);
        }
        else
        {
            Debug.LogError(string.Format("[Rect]生成失敗 x: {0} y: {1} width: {2} height: {3}", x,y,width,height));
        }
    }


    public void Create_ChildRect(int length, int dir)
    {
        if(dir == 0)
        {
            //縦に区切るときの処理
            Create_ChildRect(x, y, length, height);
            Create_ChildRect(length, y, width - length, height);
        }
        else if(dir == 1)
        {
            //横に区切るときの処理
            Create_ChildRect(x, y, width, length);
            Create_ChildRect(x, length, width, height - length);
        }
        else
        {
            Debug.LogWarning("[Rect]生成失敗");
        }
    }
    
    public void Create_ChildRect(int dir)
    {
        int l = 0;

        if (dir == 0)
        {
            l = (int)Mathf.Ceil(width / 2);
        }
        else if (dir == 1)
        {
            l = (int)Mathf.Ceil(height / 2);
        }
        else
        {
            Debug.LogWarning("[Rect]生成失敗");
        }

        Create_ChildRect(l, dir);
    }

}

