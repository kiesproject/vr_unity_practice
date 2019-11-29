using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze2 : MonoBehaviour
{
    int maze_w = 6;
    int maze_h = 6; //迷路の長さ　横と縦

    int room_num;       //部屋番号(床の数)
    public GameObject Room;       //取得用

    int wall_x_all;     //横壁の合計数
    int wall_y_all;     //縦壁の合計数
    public GameObject WallX;     //取得用
    public GameObject WallY;     //取得用

    int joints_all;    //柱の合計数
    public GameObject Joint;     //取得用

    int deleteWallNum;
    int randomNum;

    Transform transforms;

    GameObject instance;

    bool checkActive = false;

    GameObject[,] MazeConstitution;

    List<List<int>> clusteringNum;

    // Start is called before the first frame update
    void Start()
    {
        room_num = maze_w * maze_h;
        wall_x_all = maze_w * (maze_h - 1);
        wall_y_all = (maze_w - 1) * maze_h;
        joints_all = (maze_w - 1) * (maze_h - 1);

        MazeConstitution = new GameObject[maze_w * 2 + 1, maze_h * 2 + 1];

        deleteWallNum = wall_x_all + wall_y_all - ((maze_w - 1) * (maze_h - 1));
        randomNum = Random.Range(maze_w, deleteWallNum - maze_w);

        clusteringNum = new List<List<int>>();
        for(int i = 0; i < maze_w; i++)
        {
            for(int j = 0; j < maze_w; j++)
            {
                clusteringNum[i][j] = i * maze_w + j;
            }
        }

        for (int i = 0; i < maze_w + 1; i++)
        {
            float x = (float)i;
            for (int j = 0; j < maze_h; j++)
            {
                float y = (float)j;
                instance = Instantiate(WallX, new Vector3(-2.5f * (maze_w - 1) + 5.0f * j, 0.0f, 2.5f * maze_h - 5.0f * i), Quaternion.identity);
                MazeConstitution[i * 2, j * 2 + 1] = instance;
            }
        }

        for (int i = 0; i < maze_w; i++)
        {
            float x = (float)i;
            for (int j = 0; j < maze_h + 1; j++)
            {
                float y = (float)j;
                instance = Instantiate(WallY, new Vector3(-2.5f * maze_w + 5.0f * j, 0.0f, 2.5f * (maze_h - 1) - 5.0f * i), Quaternion.identity);
                MazeConstitution[i * 2 + 1, j * 2] = instance;
            }
        }

        for (int i = 0; i < maze_w; i++)
        {
            float x = (float)i;
            for (int j = 0; j < maze_h; j++)
            {
                float y = (float)j;
                instance = Instantiate(Room, new Vector3(-2.5f * (maze_w - 1.0f) + 5.0f * i, 0.0f, 2.5f * (maze_h - 1.0f) - 5.0f * j), Quaternion.identity);
                MazeConstitution[i * 2 + 1, j * 2 + 1] = instance;
            }
        }

        for (int i = 0; i < maze_w + 1; i++)
        {
            float x = (float)i;
            for (int j = 0; j < maze_h + 1; j++)
            {
                float y = (float)j;
                instance = Instantiate(Joint, new Vector3(-5.0f * (maze_w / 2.0f) + 5.0f * i, 0.0f, 5.0f * (maze_h / 2.0f) - 5.0f * j), Quaternion.identity);
                MazeConstitution[i * 2, j * 2] = instance;
            }
        }

        int delWallXNum = randomNum;
        int delWallYNum = deleteWallNum - randomNum;

        Debug.Log("delWallXNum" + delWallXNum);
        Debug.Log("delWallYNum" + delWallYNum);
        Debug.Log("deleteWallNum" + deleteWallNum);

        while (delWallXNum > 0)
        {
            while (!checkActive)
            {
                int ramx = Random.Range(1, maze_w);
                int ramy = Random.Range(1, maze_h + 1);
                Debug.Log("i:" + (ramx * 2 - 2));
                Debug.Log("j:" + (2 * ramy - 1));
                if (MazeConstitution[ramx * 2, 2 * ramy - 1].gameObject.activeSelf)
                {
                    MazeConstitution[ramx * 2, 2 * ramy -1].gameObject.SetActive(false);
                    checkActive = true;
                }
            }
            checkActive = false;
            delWallXNum--;
            //Debug.Log("delWallXNum" + delWallXNum);
        }
        
        while (delWallYNum > 0)
        {
            while (!checkActive)
            {
                int ramx = Random.Range(1, maze_w + 1);
                int ramy = Random.Range(1, maze_h);
                Debug.Log("ramx" + ramx);
                Debug.Log("ramy" + ramy);
                if (MazeConstitution[2 * ramx - 1, ramy * 2].gameObject.activeSelf)
                {
                    MazeConstitution[2 * ramx - 1, ramy * 2].gameObject.SetActive(false);
                    checkActive = true;
                }
            }
            checkActive = false;
            delWallYNum--;
            //Debug.Log("delWallYNum" + delWallYNum);
        }

        Debug.Log("delWallXNum" + delWallXNum);
        Debug.Log("delWallYNum" + delWallYNum);
        Debug.Log("deleteWallNum" + deleteWallNum);

        for(int k = 1; k < (maze_w - 1); k++)
        {
            for(int l = 1; l < (maze_h - 1); l++)
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

        //ConnectedRooms.Add(MazeConstitution[1, 1]);
        for(int k = 0; k < maze_w; k++)
        {
            for(int l = 0; l < maze_h; l++)
            {
                if (!MazeConstitution[k * 2, l * 2 + 1].activeSelf || k * 2 == 0)
                {
                    clusteringNum[k - 1][l] = clusteringNum[0][0];
                }
                if (MazeConstitution[k * 2 + 1, l * 2].activeSelf && l * 2 != 0)
                {
                    clusteringNum[k][l - 1] = clusteringNum[0][0];
                }
                if (MazeConstitution[k * 2 + 2, l * 2 + 1].activeSelf && k * 2 + 2 != maze_w * 2)
                {
                    clusteringNum[k + 1][l] = clusteringNum[0][0];
                }
                if (MazeConstitution[k * 2 + 1, l * 2 + 2].activeSelf && l * 2 + 2 != maze_h * 2)
                {
                    clusteringNum[k][l + 1] = clusteringNum[0][0];
                }
            }
        }

        for (int k = 0; k < maze_w; k++)
        {
            for (int l = 0; l < maze_h; l++)
            {
                GameObject[] surroundedWalls = new GameObject[4];
                surroundedWalls[0] = MazeConstitution[k * 2, l * 2 + 1];
                surroundedWalls[1] = MazeConstitution[k * 2 + 1, l * 2];
                surroundedWalls[2] = MazeConstitution[k * 2 + 2, l * 2 + 1];
                surroundedWalls[3] = MazeConstitution[k * 2 + 1, l * 2 + 2];

                if (clusteringNum[k][l] != 0)
                {
                    if (k * 2 == 0)
                    {
                        if(l * 2 == 0)
                        {
                            int random = Random.Range(2, 3);
                            surroundedWalls[random].SetActive(false);
                        }
                        else
                        {
                            int random = Random.Range(1, 3);
                            surroundedWalls[random].SetActive(false);
                        }
                        
                    }
                    else
                    {
                        if (l * 2 + 2 == maze_h * 2)
                        {
                            if(k * 2 + 2 == maze_w * 2)
                            {
                                int random = Random.Range(0, 1);
                                surroundedWalls[random].SetActive(false);
                            }
                            else
                            {
                                int random = Random.Range(0, 2);
                                surroundedWalls[random].SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
