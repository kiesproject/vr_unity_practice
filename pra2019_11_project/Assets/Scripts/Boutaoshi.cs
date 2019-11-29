using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//迷路を作る
public class Boutaoshi : MonoBehaviour
{
    public GameObject itemprefab;
    public GameObject wallPrefab;
    public GameObject floorPrefab;

    const int floor = 0;
    const int wall = 1;
    public int width = 15;
    public int height = 15;
    
    void Start()
    {
        int[,] maze = new int[width, height];//2次元配列
        for (int x = 0; x < width; x++)//外枠を壁にする
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == width-1 || y == height-1)
                {
                    maze[x,y] = wall;
                }
                else
                {
                    maze[x, y] = floor;
                    GameObject f = Instantiate(floorPrefab);
                    f.transform.position = new Vector3(x, -1, y);
                }

                if (maze[x, y] == wall)
                {
                    GameObject w = Instantiate(wallPrefab);//外枠の生成
                    w.transform.position = new Vector3(x, 0, y);
                }
            }
        }

        System.Random ran = new System.Random();
        for (int x = 2; x < width - 1; x += 2)//起点の配置
        {
            for (int y = 2; y < height - 1; y += 2)
            {
                maze[x, y] = wall;
                if (maze[x, y] == wall)//起点の生成
                {
                    GameObject w = Instantiate(wallPrefab);
                    w.transform.position = new Vector3(x, 0, y);
                }

                while (true)//倒していく
                {
                    int direction;

                    if (y == 2)//1番上のみ4方向
                    {
                        direction = ran.Next(4);
                    }
                    else
                    {
                        direction = ran.Next(3);
                    }
                    int LR = x;
                    int UD = y;
                    switch (direction)
                    {
                        case 0:
                            LR--;
                            break;
                        case 1:
                            LR++;
                            break;
                        case 2:
                            UD++;
                            break;
                        case 3:
                            UD--;
                            break;
                    }
                    if (maze[LR, UD] != wall)
                    {
                        maze[LR, UD] = wall;
                        GameObject w = Instantiate(wallPrefab);//壁の生成、配置
                        w.transform.position = new Vector3(LR, 0, UD);
                        break;
                    }
                }
            }
        }
    }
}
