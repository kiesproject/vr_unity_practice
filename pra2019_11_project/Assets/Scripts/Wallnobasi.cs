using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallnobasi : MonoBehaviour
{
    //壁のPrefabを取得
    public GameObject Walls;

    //生成された壁
    private GameObject Wall;

    //外周の壁の情報を入れとく
    private List<GameObject> OutWalls = new List<GameObject>();

    //拡張中の壁の情報を入れとく
    private Stack<GameObject> CurrentWalls = new Stack<GameObject>();

    //拡張開始地点の情報を入れとく
    private List<Vector3> StartWalls = new List<Vector3>();

    private List<Vector3> AllWalls = new List<Vector3>();

    private Random Random;

    bool flag1;
    bool flag2;

    // Start is called before the first frame update
    void Start()
    {
        OutWallCreate();
        AddStartWall();
    }

    private void OutWallCreate()
    {
        Vector3 WallPosition = new Vector3(-4.75f, -0.25f, -4.75f);

        //外周の壁を作成
        for (int i = 0; i <= 20; i++)
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

        for (int i = 0; i <= 19; i++)
        {
            //外周の壁生成
            Wall = Instantiate(Walls, WallPosition, transform.rotation);

            WallPosition.z -= Walls.transform.localScale.z;

            //外周の壁のListに情報を追加
            OutWalls.Add(Wall);
        }

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
        Vector3 WallStartPosition = new Vector3(-4.25f, -0.25f, 3.75f);
        StartWalls.Add(WallStartPosition);
        //外周ではない偶数座標を取得
        for (int a = 0; a <= 18; a++)
        {
            WallStartPosition.x += 0.5f;
            StartWalls.Add(WallStartPosition);
        }

        for (int b = 1; b <= 8; b++) {
            WallStartPosition.x = -4.25f;
            WallStartPosition.z -= b;
            for (int a = 0; a < 19; a++)
            {
                WallStartPosition.x += 0.5f;
                StartWalls.Add(WallStartPosition);
            }
        }
    }

    private void AddAllWall()
    {
        Vector3 AllWallPosition = new Vector3(-4.25f, -0.25f, 4.25f);
        AllWalls.Add(AllWallPosition);

        for (int a = 0; a <= 18; a++)
        {
            AllWallPosition.x += 0.5f;
            AllWalls.Add(AllWallPosition);
        }

        for (int b = 1; b <= 18; b++)
        {
            AllWallPosition.x = -4.25f;
            AllWallPosition.z -= 0.5f;
            for (int a = 0; a < 19; a++)
            {
                AllWallPosition.x += 0.5f;
                AllWalls.Add(AllWallPosition);
            }
        }
    }

    private void MazeCreate()
    {
        //StartWallsリストからランダムで座標を取得し、そこに壁を生成してCurrentWallsリストに追加
        while (StartWalls.Count > 0)
        {
            Vector3 startpos = StartWalls[Random.Range(0, StartWalls.Count)];
            Wall = Instantiate(Walls, startpos, transform.rotation);
            CurrentWalls.Push(Wall);
            StartWalls.Remove(startpos);
            MazeExpansion(startpos.x, startpos.z);
        }
    }

    //壁拡張処理
    private void MazeExpansion(float x, float z)
    {
        Vector3 currentpos = new Vector3(x, z);
        Ray ray1 = new Ray(currentpos, transform.TransformDirection(Vector3.forward));
        Ray ray2 = new Ray(currentpos, transform.TransformDirection(Vector3.back));
        Ray ray3 = new Ray(currentpos, transform.TransformDirection(Vector3.right));
        Ray ray4 = new Ray(currentpos, transform.TransformDirection(Vector3.left));
        RaycastHit hit;

        if (Physics.Raycast(ray1, out hit, currentpos.z + 1.0f))
        {
            Debug.DrawRay(ray1.origin, ray1.direction, Color.red, 3.0f);
            if (hit.collider.tag == "Wall")
            {
                flag1 = true;
            }
        }

        else if (Physics.Raycast(ray2, out hit, currentpos.z - 1.0f))
        {
            Debug.DrawRay(ray2.origin, ray2.direction, Color.red, 3.0f);
            if (hit.collider.tag == "Wall")
            {
                flag1 = true;
            }
        }

        else if (Physics.Raycast(ray3, out hit, currentpos.x + 1.0f))
        {
            Debug.DrawRay(ray3.origin, ray3.direction, Color.red, 3.0f);
            if (hit.collider.tag == "Wall")
            {
                flag1 = true;
            }
        }

        else if (Physics.Raycast(ray4, out hit, currentpos.x - 1.0f))
        {
            Debug.DrawRay(ray4.origin, ray4.direction, Color.red, 3.0f);
            if (hit.collider.tag == "Wall")
            {
                flag1 = true;
            }
        }

        else if (Physics.Raycast(ray1, out hit, currentpos.z + 2.0f))
        {
            if (CurrentWalls.Contains(hit.collider.gameObject) == true)
            {
                flag2 = true;
            }
        }

        else if (Physics.Raycast(ray2, out hit, currentpos.z - 2.0f))
        {
            if (CurrentWalls.Contains(hit.collider.gameObject) == true)
            {
                flag2 = true;
            }
        }

        else if (Physics.Raycast(ray3, out hit, currentpos.x + 2.0f))
        {
            if (CurrentWalls.Contains(hit.collider.gameObject) == true)
            {
                flag2 = true;
            }
        }

        else if (Physics.Raycast(ray4, out hit, currentpos.x - 2.0f))
        {
            if (CurrentWalls.Contains(hit.collider.gameObject) == true)
            {
                flag2 = true;
            }
        }

        MazeExpansion2();
    }

    private void MazeExpansion2()
    {
        if (flag1 == true && flag2 == true)
        {
            MazeCreate();
        }

        else
        {
            //拡張処理
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
