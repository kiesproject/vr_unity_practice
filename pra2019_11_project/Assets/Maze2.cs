using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze2 : MonoBehaviour
{
    [SerializeField]
    public int maze_w = 10;
    [SerializeField]
    public int maze_h = 10; //迷路の長さ　横と縦

    int room_num;       //部屋番号(床の数)
    public GameObject Room;       //取得用

    int wall_w_all;     //横壁の合計数
    int wall_h_all;     //縦壁の合計数
    public GameObject WallW;     //取得用
    public GameObject WallH;     //取得用

    int joints_all;    //柱の合計数
    public GameObject Joint;     //取得用

    int deleteWallNum;
    int randomNum;

    Transform transforms;

    GameObject instance;

    bool checkActive = false;

    GameObject[,] MazeConstitution;

    //List<List<int>> clusteringNum;
    int[,] clusteringNum;

    int delWallWNum;
    int delWallHNum;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化
        room_num = maze_w * maze_h;
        wall_w_all = maze_w * (maze_h - 1);
        wall_h_all = (maze_w - 1) * maze_h;
        joints_all = (maze_w - 1) * (maze_h - 1);

        MazeConstitution = new GameObject[maze_h * 2 + 1, maze_w * 2 + 1];

        deleteWallNum = wall_w_all + wall_h_all - ((maze_w - 1) * (maze_h - 1));
        randomNum = Random.Range(maze_w, deleteWallNum - maze_w);

        delWallWNum = randomNum;
        delWallHNum = deleteWallNum - randomNum;

        // クラスタリング処理用に部屋番号を割り振る
        clusteringNum = new int[maze_h, maze_w];
        for (int i = 0; i < maze_h; i++)
        {
            for (int j = 0; j < maze_w; j++)
            {
                clusteringNum[i, j] = i * maze_w + j;
            }
        }

        // 迷路を形作るようにオブジェクトを生成・配置する
        //横壁
        for (int i = 0; i < maze_h + 1; i++)
        {
            float x = (float)i;
            for (int j = 0; j < maze_w; j++)
            {
                float y = (float)j;
                instance = Instantiate(WallW, new Vector3(-2.5f * (maze_w - 1) + 5.0f * j, 0.0f, 2.5f * maze_h - 5.0f * i), Quaternion.identity);
                MazeConstitution[i * 2, j * 2 + 1] = instance;
            }
        }
        //縦壁
        for (int i = 0; i < maze_h; i++)
        {
            float x = (float)i;
            for (int j = 0; j < maze_w + 1; j++)
            {
                float y = (float)j;
                instance = Instantiate(WallH, new Vector3(-2.5f * maze_w + 5.0f * j, 0.0f, 2.5f * (maze_h - 1) - 5.0f * i), Quaternion.identity);
                MazeConstitution[i * 2 + 1, j * 2] = instance;
            }
        }
        //部屋(床)
        for (int i = 0; i < maze_h; i++)
        {
            float x = (float)i;
            for (int j = 0; j < maze_w; j++)
            {
                float y = (float)j;
                instance = Instantiate(Room, new Vector3(-2.5f * (maze_w - 1.0f) + 5.0f * j, 0.0f, 2.5f * (maze_h - 1.0f) - 5.0f * i), Quaternion.identity);
                MazeConstitution[i * 2 + 1, j * 2 + 1] = instance;
            }
        }
        //支柱
        for (int i = 0; i < maze_h + 1; i++)
        {
            float x = (float)i;
            for (int j = 0; j < maze_w + 1; j++)
            {
                float y = (float)j;
                instance = Instantiate(Joint, new Vector3(-5.0f * (maze_w / 2.0f) + 5.0f * j, 0.0f, 5.0f * (maze_h / 2.0f) - 5.0f * i), Quaternion.identity);
                MazeConstitution[i * 2, j * 2] = instance;
            }
        }


        //Debug.Log("delWallXNum" + delWallXNum);
        //Debug.Log("delWallYNum" + delWallYNum);
        //Debug.Log("deleteWallNum" + deleteWallNum);

        //余分な壁を消去する処理 迷路ごとに決められた消去する壁の最大数()をランダムで分ける。それに応じて縦壁と横壁をランダムに消去する。
        //横壁の消去
        while (delWallWNum > 0)
        {
            while (!checkActive)
            {
                int ranw = Random.Range(1, maze_w + 1);
                int ranh = Random.Range(1, maze_h);
                //Debug.Log("i:" + (ramx * 2 - 2));
                //Debug.Log("j:" + (2 * ramy - 1));
                if (MazeConstitution[ranh * 2, 2 * ranw - 1].gameObject.activeSelf)
                {
                    MazeConstitution[ranh * 2, 2 * ranw - 1].gameObject.SetActive(false);
                    checkActive = true;
                }
            }
            checkActive = false;
            delWallWNum--;
            //Debug.Log("delWallXNum" + delWallXNum);
        }
        // 縦壁の消去
        while (delWallHNum > 0)
        {
            while (!checkActive)
            {
                int ranw = Random.Range(1, maze_w);
                int ranh = Random.Range(1, maze_h + 1);
                //Debug.Log("ramx" + ramx);
                //Debug.Log("ramy" + ramy);
                if (MazeConstitution[2 * ranh - 1, ranw * 2].gameObject.activeSelf)
                {
                    MazeConstitution[2 * ranh - 1, ranw * 2].gameObject.SetActive(false);
                    checkActive = true;
                }
            }
            checkActive = false;
            delWallHNum--;
            //Debug.Log("delWallYNum" + delWallYNum);
        }

        //Debug.Log("delWallXNum" + delWallXNum);
        //Debug.Log("delWallYNum" + delWallYNum);
        //Debug.Log("deleteWallNum" + deleteWallNum);

        RepairingMaze();
        CheckMaze();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RepairingMaze()
    {
        AddWall();
        DeleteSurroundedWall();
        //CheckMaze();
    }

    //支柱(joint)に4つの壁が1つも接していなければランダムに壁を一つ増やす処理
    public void AddWall()
    {
        for (int k = 1; k < maze_h; k++)
        {
            for (int l = 1; l < maze_w; l++)
            {
                GameObject[] crossedWalls = new GameObject[4];
                crossedWalls[0] = MazeConstitution[k * 2, l * 2 - 1];
                crossedWalls[1] = MazeConstitution[k * 2 - 1, l * 2];
                crossedWalls[2] = MazeConstitution[k * 2, l * 2 + 1];
                crossedWalls[3] = MazeConstitution[k * 2 + 1, l * 2];

                if (!crossedWalls[0].activeSelf && !crossedWalls[1].activeSelf && !crossedWalls[2].activeSelf && !crossedWalls[3].activeSelf)
                {
                    int randomNum = Random.Range(0, 3);
                    crossedWalls[randomNum].SetActive(true);
                }
            }
        }
    }

    //周囲を壁で囲まれた部屋(迷路として繋がっていない部屋)を発見し、その周囲の壁をランダムで消去する処理
    public void DeleteSurroundedWall()
    {
        for (int x = 0; x < maze_h; x++)
        {
            for (int z = 0; z < maze_w; z++)
            {
                GameObject[] surroundedWalls = new GameObject[4];
                //0:北, 1:西, 2:南, 3:東
                surroundedWalls[0] = MazeConstitution[x * 2, z * 2 + 1];
                surroundedWalls[1] = MazeConstitution[x * 2 + 1, z * 2];
                surroundedWalls[2] = MazeConstitution[x * 2 + 2, z * 2 + 1];
                surroundedWalls[3] = MazeConstitution[x * 2 + 1, z * 2 + 2];

                GameObject[] delWalls;
                int random;

                Debug.Log("北:" + surroundedWalls[0].activeSelf);
                Debug.Log("西:" + surroundedWalls[1].activeSelf);
                Debug.Log("南:" + surroundedWalls[2].activeSelf);
                Debug.Log("東:" + surroundedWalls[3].activeSelf);

                if (surroundedWalls[0].activeSelf && surroundedWalls[1].activeSelf && surroundedWalls[2].activeSelf && surroundedWalls[3].activeSelf)
                {
                    if (x == 0)
                    {
                        if (z == 0)
                        {
                            random = Random.Range(2, 3);
                            surroundedWalls[random].SetActive(false);
                        }
                        else if (z * 2 + 2 == maze_w * 2)
                        {
                            random = Random.Range(1, 2);
                            surroundedWalls[random].SetActive(false);
                        }
                        else
                        {
                            random = Random.Range(1, 3);
                            surroundedWalls[random].SetActive(false);
                        }
                    }
                    else if (z == 0)
                    {
                        if (x * 2 + 2 == maze_h * 2)
                        {
                            delWalls = new GameObject[2] { surroundedWalls[0], surroundedWalls[3] };
                            random = Random.Range(0, 1);
                            delWalls[random].SetActive(false);
                        }
                        else
                        {
                            delWalls = new GameObject[3] { surroundedWalls[0], surroundedWalls[2], surroundedWalls[3] };
                            random = Random.Range(0, 2);
                            delWalls[random].SetActive(false);
                        }
                    }
                    else if (x * 2 + 2 == maze_h * 2)
                    {
                        if (z * 2 + 2 == maze_w * 2)
                        {
                            random = Random.Range(0, 1);
                            surroundedWalls[random].SetActive(false);
                        }
                        else
                        {
                            delWalls = new GameObject[3] { surroundedWalls[0], surroundedWalls[1], surroundedWalls[3] };
                            random = Random.Range(0, 2);
                            delWalls[random].SetActive(false);
                        }
                    }
                    else if (z * 2 + 2 == maze_w * 2)
                    {
                        random = Random.Range(0, 2);
                        surroundedWalls[random].SetActive(false);
                    }
                    else
                    {
                        random = Random.Range(0, 3);
                        surroundedWalls[random].SetActive(false);
                    }
                }
                //if (clusteringNum[x, z] != 0)
                //{
                //if (x == 0 || z == 0)
                //{
                //if (x * 2 + 2 == maze_h * 2)
                //{
                //if(clusteringNum[x - 1, z] == 0 && clusteringNum[x, z + 1] != 0)
                //{
                //surroundedWalls[0].SetActive(false);
                //}
                //else if(clusteringNum[x - 1, z] != 0 && clusteringNum[x, z + 1] == 0)
                //{
                //surroundedWalls[3].SetActive(false);
                //}
                //else
                //{
                //GameObject[] delWalls = new GameObject[2] { surroundedWalls[0], surroundedWalls[3] };
                //int random = Random.Range(0, 1);
                //delWalls[random].SetActive(false);
                //}
                //}
                //else if (z * 2 + 2 == maze_w * 2)
                //{
                //if (clusteringNum[x, z - 1] == 0 && clusteringNum[x + 1, z] != 0)
                //{
                //surroundedWalls[1].SetActive(false);
                //clusteringNum[x + 1, z] = 0;
                //}
                //else if (clusteringNum[x, z - 1] != 0 && clusteringNum[x + 1, z] == 0)
                //{
                //surroundedWalls[2].SetActive(false);
                //clusteringNum[x, z - 1] = 0;
                //}
                //else
                //{
                //int random = Random.Range(1, 2);
                //surroundedWalls[random].SetActive(false);
                //}
                //}
                //else
                //{
                //if(x == 0 && z != 0)
                //{
                //int random = Random.Range(1, 3);
                //surroundedWalls[random].SetActive(false);
                //}
                //else if(x != 0 && z==0)
                //{
                //GameObject[] delWalls = new GameObject[3] { surroundedWalls[0], surroundedWalls[2], surroundedWalls[3] };
                //int random = Random.Range(0, 2);
                //delWalls[random].SetActive(false);
                //}
                //else
                //{
                //int random = Random.Range(2, 3);
                //surroundedWalls[random].SetActive(false);
                //}
                //}
                //}
                //else if(x * 2 + 2 == maze_h * 2 || z * 2 + 2 == maze_w * 2)
                //{
                //if (x == maze_h * 2 && z != maze_w * 2)
                //{
                //GameObject[] delWalls = new GameObject[3] { surroundedWalls[0], surroundedWalls[1], surroundedWalls[3] };
                //int random = Random.Range(0, 2);
                //delWalls[random].SetActive(false);
                //}
                //else if (x != maze_h * 2 && z == maze_w * 2)
                //{
                //int random = Random.Range(0, 2);
                //surroundedWalls[random].SetActive(false);
                //}
                //else
                //{
                //int random = Random.Range(2, 3);
                //surroundedWalls[random].SetActive(false);
                //}
                //}
                //else
                //{
                //int random = Random.Range(0, 3);
                //surroundedWalls[random].SetActive(false);
                //}
                //}
            }
        }
    }

    public void CheckMaze()
    {
        // クラスタリング処理：左上から順に隣接する部屋の番号を左上の部屋番号(0)に揃える　→　部屋同士がつながっている
        int counter = 5;
        while (counter < 0)
        {
            for (int k = 0; k < maze_h; k++)
            {
                for (int l = 0; l < maze_w; l++)
                {
                    //部屋に対し北の壁
                    if (k != 0 && !MazeConstitution[k * 2, l * 2 + 1].activeSelf)
                    {
                        //clusteringNum[k - 1][l] = clusteringNum[0][0];
                        clusteringNum[k - 1, l] = clusteringNum[k, l];
                    }
                    //部屋に対し西の壁
                    if (l != 0 && !MazeConstitution[k * 2 + 1, l * 2].activeSelf)
                    {
                        //clusteringNum[k][l - 1] = clusteringNum[0][0];
                        clusteringNum[k, l - 1] = clusteringNum[k, l];
                    }
                    //部屋に対し南の壁
                    if (k * 2 + 2 != maze_h * 2 && !MazeConstitution[k * 2 + 2, l * 2 + 1].activeSelf)
                    {
                        //clusteringNum[k + 1][l] = clusteringNum[0][0];
                        clusteringNum[k + 1, l] = clusteringNum[k, l];
                    }
                    //部屋に対し東の壁
                    if (l * 2 + 2 != maze_w * 2 && !MazeConstitution[k * 2 + 1, l * 2 + 2].activeSelf)
                    {
                        //clusteringNum[k][l + 1] = clusteringNum[0][0];
                        clusteringNum[k, l + 1] = clusteringNum[k, l];
                    }
                }
            }
            counter--;
        }
        //foreach(int i in clusteringNum)
        //{
        //if(i != 0)
        //{
        //RepairingMaze();
        //}
        //}
    }
}
