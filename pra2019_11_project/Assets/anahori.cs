using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anahori : MonoBehaviour
{
    int[,] kabe;
    int xSize = 11;
    int ySize = 11;
    int hotta;
    List<Vector2> routedList = new List<Vector2>();
    public GameObject oonosuke;


    // Start is called before the first frame update
    void Start()
    {
        //配列を呼び出すよ
        kabe = new int[xSize, ySize];
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                kabe[i, j] = 0;
            }
        }
        int Startx = 1;
        int Starty = 1;
        int Up, Down, Right, Left;

        int q = 0;
        while (Goal(kabe))
        {
            if (1000 < q++) { Debug.LogError("ヨシ！！"); break; }

            Anahori(Startx, Starty);
            int r = Random.Range(0, routedList.Count);
            Startx = (int)routedList[r].x;
            Starty = (int)routedList[r].y;
        }

        string s = "";
        for (int d = 0; d < xSize; d++)
        {
            for (int e = 0; e < ySize; e++)
            {
                s += kabe[d, e].ToString();
                if (kabe[d, e] == 0)
                {
                    Instantiate(oonosuke, new Vector3(d, 0, e), Quaternion.identity);
                }
            }
            s += "\n";
        }
        Debug.Log(s);

    }



    // Update is called once per frame
    void Update()
    {

    }

    bool IsOutRange(int x, int y)                   //指定した配列の座標が範囲外かみるお
    {
        if (x < 0 || y < 0 || x > xSize-1 || y > ySize-1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool Goal(int[,] z)
    {
        int tuurodayo = 0;
        for (int a = 0; 2 * a + 1 < xSize; a++)
        {
            for (int b = 0; 2 * b + 1 < ySize; b++)
            {
                if (z[2 * a + 1, 2 * b + 1] == 1)
                {
                    tuurodayo += 1;
                    
                }

            }
        }
        if (tuurodayo == (xSize / 2) * (ySize / 2))
        {
            return false;
        }
        else return true;
    }

    void Anahori(int startx, int starty)
    {
        bool kisuuhanntei = false;
        while (kisuuhanntei == false)
        {
            List<int> dirlist = new List<int> { 0, 1, 2, 3 };        //いろんなしょきちー
            kabe[startx, starty] = 1;
            hotta = 0;

            if (IsOutRange(startx, starty - 2))     //2マス先がダメだったらdirlistから外す
            {
                Debug.Log("除外された！");
                dirlist.Remove(0);
            }
            if (IsOutRange(startx + 2, starty))
            {
                dirlist.Remove(1);
            }
            if (IsOutRange(startx, starty + 2))
            {
                dirlist.Remove(2);
            }
            if (IsOutRange(startx - 2, starty))
            {
                dirlist.Remove(3);
            }


            while (hotta == 0)                    //掘るまで繰り返すお
            {
                int houkou = dirlist[Random.Range(0, dirlist.Count)];      //厳選した方向をランダムで呼び出す
                Debug.Log("houkou: " + houkou);
                    

                if (houkou == 0)                          //掘る(ほもぉ)
                {
                    if (kabe[startx, starty - 2] == 0)
                    {
                        kabe[startx, starty - 1] = 1;
                        hotta = 1;
                        starty -= 2;
                        routedList.Add(new Vector2(startx, starty));

                    }
                    else { dirlist.Remove(0); }
                }

                if (houkou == 1)
                {
                    Debug.Log(string.Format("[{0}, {1}]", startx + 2, starty));
                    if (kabe[startx + 2, starty] == 0)
                    {
                        kabe[startx + 1, starty] = 1;
                        hotta = 1;
                        startx += 2;
                        routedList.Add(new Vector2(startx, starty));
                    }
                    else { dirlist.Remove(1); }
                }

                if (houkou == 2)
                {
                    if (kabe[startx, starty + 2] == 0)
                    {
                        kabe[startx, starty + 1] = 1;
                        hotta = 1;
                        starty += 2;
                        routedList.Add(new Vector2(startx, starty));
                    }
                    else { dirlist.Remove(2); }
                }

                if (houkou == 3)
                {
                    if (kabe[startx - 2, starty] == 0)
                    {
                        kabe[startx - 1, starty] = 1;
                        hotta = 1;
                        startx -= 2;
                        routedList.Add(new Vector2(startx, starty));
                    }
                    else { dirlist.Remove(3); }
                }
                if (dirlist.Count == 0)
                {
                    kisuuhanntei = true;
                    break;
                }
            }


        }
    }
    
}
    


