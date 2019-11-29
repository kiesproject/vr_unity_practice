using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    [Header("床のオブジェクト")]
    [SerializeField]
    private GameObject floor;

    [Header("壁のオブジェクト")]
    [SerializeField]
    private GameObject wall;

    [Header("外壁のオブジェクト")]
    [SerializeField]
    private GameObject iron;

    [Header("プレイヤーのトランスフォーム")]
    [SerializeField]
    private Transform Player;

    [Header("マップ全体の大きさ")]
    [SerializeField]
    [Range(20, 100)]
    int MapWidth = 50;
    [SerializeField]
    [Range(20, 100)]
    int MapHeight = 30;

    [Header("壁の高さ")]
    [SerializeField]
    int WallHeght = 2;

    const int wallID = 0;
    const int roomID = 1;
    const int roadID = 2;

    private int[,] Map;
    [Header("部屋の数 Min,Max（最小,最大）")]
    [SerializeField]
    [Range(1, 10)]
    int MinRooms = 1;
    [SerializeField]
    [Range(1, 20)]
    int MaxRooms = 13;

    private int roomNum;


    private List<DviRoomInfomation> RoomDVI = new List<DviRoomInfomation>();


    // Start is called before the first frame update
    void Start()
    {
        MapResetData();
        MapDivisionCreate();
        RoomCreate();
        RoadCreate();
        CreateDangeon();
        InitPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 壁しかないMapデータの生成
    private void MapResetData()
    {
        Map = new int[MapWidth, MapHeight]; //Mapデータ[横,縦]

        // 壁しかないMapデータを作る -------------------
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                Map[i, j] = wallID;
            }
        }
    }

    // 最初の区画の情報を作る
    private void MapDivisionCreate()
    {
        int dviPos;
        roomNum = Random.Range(MinRooms, MaxRooms);
        RoomDVI.Add(new DviRoomInfomation());
        RoomDVI[0].Top = 0;
        RoomDVI[0].Left = 0;
        RoomDVI[0].Bottom = MapHeight - 1;
        RoomDVI[0].Right = MapWidth - 1;
        RoomDVI[0].areaRank = RoomDVI[0].Bottom + RoomDVI[0].Right;

        for (int i = 1; i < roomNum; i++)
        {
            RoomDVI.Add(new DviRoomInfomation());
            int Target = 0;
            int AreaMax = 0;
            // 最大の面積を持つ区画を指定する
            for (int j = 0; j < i; j++)
            {
                if (RoomDVI[j].areaRank >= AreaMax)
                {
                    AreaMax = RoomDVI[j].areaRank;
                    Target = j;
                }
            }

            // 分割点を求める
            if ((RoomDVI[Target].Bottom - RoomDVI[Target].Top) > 12 && (RoomDVI[Target].Right - RoomDVI[Target].Left) > 12)
            {
                RoomDVI[i].nextRoom = Target;
                dviPos = Random.Range(0, RoomDVI[Target].areaRank);

                if (dviPos > (RoomDVI[Target].Bottom - RoomDVI[Target].Top))
                {
                    RoomDVI[i].Left = RoomDVI[Target].Left + Random.Range(6, RoomDVI[Target].Right - RoomDVI[Target].Left - 6);
                    RoomDVI[i].Right = RoomDVI[Target].Right;
                    RoomDVI[Target].Right = RoomDVI[i].Left - 1;
                    RoomDVI[i].Top = RoomDVI[Target].Top;
                    RoomDVI[i].Bottom = RoomDVI[Target].Bottom;
                    RoomDVI[i].isNextX = true;
                    RoomDVI[i].NextRoomPos = RoomDVI[i].Left;
                }
                else
                {
                    RoomDVI[i].Top = RoomDVI[Target].Top + Random.Range(6, RoomDVI[Target].Bottom - RoomDVI[Target].Top - 6);
                    RoomDVI[i].Bottom = RoomDVI[Target].Bottom;
                    RoomDVI[Target].Bottom = RoomDVI[i].Top - 1;
                    RoomDVI[i].Left = RoomDVI[Target].Left;
                    RoomDVI[i].Right = RoomDVI[Target].Right;
                    RoomDVI[i].isNextX = false;
                    RoomDVI[i].NextRoomPos = RoomDVI[i].Top;

                }

                RoomDVI[i].areaRank = RoomDVI[i].Bottom - RoomDVI[i].Top + RoomDVI[i].Right - RoomDVI[i].Left;
                RoomDVI[Target].areaRank = RoomDVI[Target].Bottom - RoomDVI[Target].Top + RoomDVI[Target].Right - RoomDVI[Target].Left;
            }
            else
            {
                roomNum = i;
                break;
            }
        }
    }

    private void RoomCreate()
    {
        int ratioX;
        int ratioY;
        int moveX;
        int moveY;
        for (int i = 0; i < roomNum; i++)
        {
            if (RoomDVI[i] != null)
            {
                ratioY = RoomDVI[i].Bottom - RoomDVI[i].Top;
                ratioY = Mathf.FloorToInt(Random.Range(0.500f, 0.800f) * ratioY);
                ratioX = RoomDVI[i].Right - RoomDVI[i].Left;
                ratioX = Mathf.FloorToInt(Random.Range(0.500f, 0.800f) * ratioX);
                moveY = Mathf.FloorToInt((RoomDVI[i].Bottom - RoomDVI[i].Top - ratioY) / 2.0f);
                moveX = Mathf.FloorToInt((RoomDVI[i].Right - RoomDVI[i].Left - ratioX) / 2.0f);
                RoomDVI[i].Top = RoomDVI[i].Top + moveY;
                RoomDVI[i].Bottom = RoomDVI[i].Top + ratioY;
                RoomDVI[i].Left = RoomDVI[i].Left + moveX;
                RoomDVI[i].Right = RoomDVI[i].Left + ratioX;

                for (int j = 0; j < ratioY; j++)
                {
                    for (int k = 0; k < ratioX; k++)
                    {
                        Map[RoomDVI[i].Left + k + 1, RoomDVI[i].Top + j + 1] = roomID;
                    }
                }
            }
            else
                break;
        }
    }

    private void RoadCreate()
    {
        int NOWpos = 0;
        int NOWdis = 0;
        int NEXTpos = 0;
        int NEXTdis = 0;
        for (int i = 1; i < roomNum; i++)
        {
            if (RoomDVI[i].isNextX)
            {
                // 通路の開始地点、及び終了地点を求める
                NOWpos = RoomDVI[i].Bottom - RoomDVI[i].Top;
                NOWpos = Random.Range(1, NOWpos) + RoomDVI[i].Top;
                NEXTpos = RoomDVI[RoomDVI[i].nextRoom].Bottom - RoomDVI[RoomDVI[i].nextRoom].Top;
                NEXTpos = Random.Range(1, NEXTpos) + RoomDVI[RoomDVI[i].nextRoom].Top;

                // 通路を引く線の長さを（開始、終了地点共に）求める
                NOWdis = RoomDVI[i].Left - RoomDVI[i].NextRoomPos + 1;
                NEXTdis = RoomDVI[i].NextRoomPos - RoomDVI[RoomDVI[i].nextRoom].Right + 1;


                // ←ライン作成
                for (int j = 0; j < NOWdis; j++)
                {
                    Map[-j + RoomDVI[i].Left, NOWpos] = roadID;
                }

                // →ライン作成
                for (int j = 0; j < NEXTdis; j++)
                {
                    Map[j + RoomDVI[RoomDVI[i].nextRoom].Right, NEXTpos] = roadID;
                }

                // 縦ライン作成
                for (int j = 0; ; j++)
                {
                    // NOWとNEXT、どちらの方が高さが大きいか調べ、縦ラインを作成する
                    if (NOWpos >= NEXTpos)
                    {
                        if ((NEXTpos + j) < NOWpos)
                        {
                            Map[RoomDVI[i].NextRoomPos, NEXTpos + j] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if ((NOWpos + j) < NEXTpos)
                        {
                            Map[RoomDVI[i].NextRoomPos, NOWpos + j] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

            }
            else
            {
                // 通路の開始地点、及び終了地点を求める
                NOWpos = RoomDVI[i].Right - RoomDVI[i].Left;
                NOWpos = Random.Range(1, NOWpos) + RoomDVI[i].Left;
                NEXTpos = RoomDVI[RoomDVI[i].nextRoom].Right - RoomDVI[RoomDVI[i].nextRoom].Left;
                NEXTpos = Random.Range(1, NEXTpos) + RoomDVI[RoomDVI[i].nextRoom].Left;

                // 通路を引く線の長さを（開始、終了地点共に）求める
                NOWdis = RoomDVI[i].Top - RoomDVI[i].NextRoomPos + 1;
                NEXTdis = RoomDVI[i].NextRoomPos - RoomDVI[RoomDVI[i].nextRoom].Bottom + 1;


                // ↑ライン作成
                for (int j = 0; j < NOWdis; j++)
                {
                    Map[NOWpos, -j + RoomDVI[i].Top] = roadID;
                }

                // ↓ライン作成
                for (int j = 0; j < NEXTdis; j++)
                {
                    Map[NEXTpos, j + RoomDVI[RoomDVI[i].nextRoom].Bottom] = roadID;
                }

                // 横ライン作成
                for (int j = 0; ; j++)
                {
                    // NOWとNEXT、どちらの方が幅が大きいか調べ、横ラインを作成する
                    if (NOWpos >= NEXTpos)
                    {
                        if ((NEXTpos + j) < NOWpos)
                        {
                            Map[NEXTpos + j, RoomDVI[i].NextRoomPos] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if ((NOWpos + j) < NEXTpos)
                        {
                            Map[NOWpos + j, RoomDVI[i].NextRoomPos] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

    }

    private void CreateDangeon()
    {
        for (int i = 0; i < MapWidth; i++)
        {
            for (int j = 0; j < MapHeight; j++)
            {
                // 床を敷き詰める
                Instantiate(floor, new Vector3(i - MapWidth / 2, 0, j - MapHeight / 2), Quaternion.identity);
                // 壁だった場合壁にする
                if (Map[i, j] == wallID)
                {
                    for (int height = 0; height < WallHeght; height++)
                    {
                        Instantiate(wall, new Vector3(i - MapWidth / 2, height + 1, j - MapHeight / 2), Quaternion.identity);
                    }
                }
            }
        }

        // 外壁を作る
        for (int i = -1; i < MapHeight + 1; i++)
        {
            for (int j = 0; j < WallHeght + 1; j++)
            {
                Instantiate(iron, new Vector3(-1 - MapWidth / 2, j, i - MapHeight / 2), Quaternion.identity);
                Instantiate(iron, new Vector3(MapWidth - MapWidth / 2, j, i - MapHeight / 2), Quaternion.identity);
            }

        }
        for (int i = -1; i < MapWidth; i++)
        {
            for (int j = 0; j < WallHeght + 1; j++)
            {
                Instantiate(iron, new Vector3(i - MapWidth / 2, j, -1 - MapHeight / 2), Quaternion.identity);
                Instantiate(iron, new Vector3(i - MapWidth / 2, j, MapHeight - MapHeight / 2), Quaternion.identity);
            }
        }

    }

    private void InitPlayer()
    {
        int InitRoom = Random.Range(0, roomNum);
        int x = Random.Range(0, RoomDVI[InitRoom].Right - RoomDVI[InitRoom].Left) + RoomDVI[InitRoom].Left;
        int z = Random.Range(0, RoomDVI[InitRoom].Bottom - RoomDVI[InitRoom].Top) + RoomDVI[InitRoom].Top;
        Player.transform.position = new Vector3(x - MapWidth / 2 + 1, 0.5f, z - MapHeight / 2 + 1);
    }
}



public class DviRoomInfomation
{
    public int Top = 0;
    public int Left = 0;
    public int Bottom = 0;
    public int Right = 0;
    public int areaRank = 0;
    public int nextRoom = 0;
    public bool isNextX = false;
    public int NextRoomPos = 0;
}