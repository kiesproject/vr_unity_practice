using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taosi : MonoBehaviour
{
    //壁のPrefabを取得
    public GameObject Walls;

    //生成された壁
    private GameObject Wall;

    //外周の壁の情報を入れとく
    private List<GameObject> OutWalls = new List<GameObject>();

    //拡張中の壁の情報を入れとく
    private List<Vector3> CurrentWalls = new List<Vector3>();

    //拡張開始地点の情報を入れとく
    private List<Vector3> StartWalls = new List<Vector3>();

    private List<Vector3> Road = new List<Vector3>();

    private Random Random;

    // Start is called before the first frame update
    void Start()
    {
        OutWallCreate();
        AddRoad();
        AddStartWall();
    }

    private void OutWallCreate()
    {
        Vector3 WallPosition = new Vector3(-5.0f, 0f, -4.0f);

        //外周の壁を作成
        for (int i = 0; i <= 18; i++)
        {
            //外周の壁生成
            Wall = Instantiate(Walls, WallPosition, transform.rotation);

            WallPosition.z += Walls.transform.localScale.z;

            //外周の壁のListに情報を追加
            OutWalls.Add(Wall);
        }

        WallPosition.z -= Walls.transform.localScale.z;

        for (int i = 0; i <= 19; i++)
        {
            //外周の壁生成
            Wall = Instantiate(Walls, WallPosition, transform.rotation);

            WallPosition.x += Walls.transform.localScale.x;

            //外周の壁のListに情報を追加
            OutWalls.Add(Wall);
        }

        for (int i = 0; i <= 18; i++)
        {
            //外周の壁生成
            Wall = Instantiate(Walls, WallPosition, transform.rotation);

            WallPosition.z -= Walls.transform.localScale.z;

            //外周の壁のListに情報を追加
            OutWalls.Add(Wall);
        }

        WallPosition.z += Walls.transform.localScale.z;

        for (int i = 0; i <= 19; i++)
        {
            //外周の壁生成
            Wall = Instantiate(Walls, WallPosition, transform.rotation);

            WallPosition.x -= Walls.transform.localScale.x;

            //外周の壁のListに情報を追加
            OutWalls.Add(Wall);
        }
    }

    private void AddStartWall()
    {
        for (int b = 0; b <= 7; b++)
        {
            Vector3 WallStartPosition = new Vector3(-4.0f, 0f, 4.0f);
            WallStartPosition.z -= b;
            StartWalls.Add(WallStartPosition);
            Road.Remove(WallStartPosition);
            Wall = Instantiate(Walls, WallStartPosition, transform.rotation);
            //外周ではない偶数座標を取得
            for (int a = 0; a <= 7; a++)
            {
                WallStartPosition.x += 1.0f;
                StartWalls.Add(WallStartPosition);
                Road.Remove(WallStartPosition);
                Wall = Instantiate(Walls, WallStartPosition, transform.rotation);
            }
        }
    }

    private void AddRoad()
    {
        Vector3 RoadPosition = new Vector3(-4.5f, 0f, 4.5f);
        Road.Add(RoadPosition);

        for (int a = 0; a <= 17; a++)
        {
            RoadPosition.x += 0.5f;
            Road.Add(RoadPosition);
        }

        for (int b = 1; b <= 17; b++)
        {
            RoadPosition.x = -4.5f;
            RoadPosition.z -= 0.5f;
            Road.Add(RoadPosition);
            for (int a = 0; a < 17; a++)
            {
                RoadPosition.x += 0.5f;
                Road.Add(RoadPosition);
            }
        }
    }

    private void CreateMaze()
    {
        for (int i = 0; i <= 8; i++)
        {
            Vector3 currentWallpos = StartWalls[i];
            Vector3 Uppos = currentWallpos + new Vector3(0.0f, 0.0f, 0.5f);
            Vector3 Downpos = currentWallpos + new Vector3(0.0f, 0.0f, -0.5f);
            Vector3 Rightpos = currentWallpos + new Vector3(0.5f, 0.0f, 0.0f);
            Vector3 Leftpos = currentWallpos + new Vector3(-0.5f, 0.0f, 0.0f);
            CurrentWalls.Add(Uppos);
            CurrentWalls.Add(Downpos);
            CurrentWalls.Add(Rightpos);
            CurrentWalls.Add(Leftpos);
            
        }

        for (int i = 9; i <= 71; i++)
        {
            Vector3 currentWallpos = StartWalls[i];
            Vector3 Uppos = currentWallpos + new Vector3(0.0f, 0.0f, 0.5f);
            Vector3 Downpos = currentWallpos + new Vector3(0.0f, 0.0f, -0.5f);
            Vector3 Rightpos = currentWallpos + new Vector3(0.5f, 0.0f, 0.0f);
            Vector3 Leftpos = currentWallpos + new Vector3(-0.5f, 0.0f, 0.0f);
            CurrentWalls.Add(Uppos);
            CurrentWalls.Add(Downpos);
            CurrentWalls.Add(Rightpos);
            CurrentWalls.Add(Leftpos);
            
        }
    }
}
