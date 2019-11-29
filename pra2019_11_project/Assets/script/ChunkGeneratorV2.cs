using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneratorV2 : MonoBehaviour
{
    /// <summary>
    /// 生成時のマップのデータ
    /// </summary>
    public struct ChunkData
    {
        // チャンクの識別番号
        public int ChunkIndex;
        // どの方向に進めるかのデータ
        public bool[] CanMove;
        // リセット用のメソッド
        public void ResetMoveData()
        {
            for (int k = 0; k < CanMove.Length; k++)
            {
                CanMove[k] = false;
            }
        }
    }

    const int Room = 0; // 部屋
    const int Path = 1; // 通路
    const int OutSideWall = 2;  // 外壁
    
    // 進める方向の定義
    private enum Dirction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    // つぎに掘り進められる方向を格納
    private List<Vector2Int> StartCells;

    // チャンクプレファブ
    [SerializeField] GameObject[] Chunks;

    // mapの生成サイズ
    [SerializeField] int MapX = 6, MapZ = 4;

    // データは2次元配列で
    public ChunkData[,] mapData;

    // GameControllerでの制御
    [SerializeField] GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        ResetMapData();

        CreateStageData();

        CreateStage();

        DebugData();

        gameController.GoReady();
    }

    /// <summary>
    /// マップデータ初期化
    /// </summary>
    private void ResetMapData()
    {
        mapData = new ChunkData[MapX, MapZ];
        StartCells = new List<Vector2Int>();
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                mapData[i, j].ChunkIndex = Room;

                mapData[i, j].CanMove = new bool[4];
                mapData[i, j].ResetMoveData();
            }
        }
    }

    /// <summary>
    /// データの生成
    /// </summary>
    private void CreateStageData()
    {
        //　壁の判定のため、外側は通路に
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                if(i == 0 || j == 0 || i == MapX - 1 || j == MapZ - 1)
                {
                    mapData[i, j].ChunkIndex = Path;
                }
                else
                {
                    mapData[i, j].ChunkIndex = Room;
                }
            }
        }

        // 穴掘り開始
        Dig(1, 1);


        // 外壁を生成
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                if (i == 0)
                {
                    mapData[i, j].CanMove[(int)Dirction.Left] = true;
                }
                
                if(j == 0)
                {
                    mapData[i, j].CanMove[(int)Dirction.Down] = true;
                }
                
                if(i == MapX - 1)
                {
                    mapData[i, j].CanMove[(int)Dirction.Right] = true;
                }
                
                if(j == MapZ - 1)
                {
                    mapData[i, j].CanMove[(int)Dirction.Up] = true;
                }

                if (i == 0 || j == 0 || i == MapX - 1 || j == MapZ - 1)
                {
                    mapData[i, j].ChunkIndex = OutSideWall;
                    for (int k = 0; k < mapData[i, j].CanMove.Length; k++) mapData[i, j].CanMove[k] = !mapData[i, j].CanMove[k];
                }

                // スタート時点近くは壁を生成しない
                if(i == 1 && j == 1)
                {
                    mapData[i, j].CanMove = new bool[] { true, true, true, true };
                }
            }
        }
    }

    /// <summary>
    /// 穴掘り法を適用して通路を生成
    /// </summary>
    /// <param name="x">マップのX座標</param>
    /// <param name="z">マップのZ座標</param>
    private void Dig(int x, int z)
    {
        while (true)
        {
            List<Dirction> directions = new List<Dirction>();

            // 1つ先かつ2つ先が通路でなければ方向を選択
            if (mapData[x, z - 1].ChunkIndex == Room && mapData[x, z - 2].ChunkIndex == Room)
                directions.Add(Dirction.Down);

            if (mapData[x + 1, z].ChunkIndex == Room && mapData[x + 2, z].ChunkIndex == Room)
                directions.Add(Dirction.Right);

            if (mapData[x, z + 1].ChunkIndex == Room && mapData[x, z + 2].ChunkIndex == Room)
                directions.Add(Dirction.Up);

            if (mapData[x - 1, z].ChunkIndex == Room && mapData[x - 2, z].ChunkIndex == Room)
                directions.Add(Dirction.Left);

            if (directions.Count == 0) break;   // 掘り進められなければループを抜ける

            // スタート位置に穴を
            SetPath(x, z);

            // ランダムな方向に２つ分進める
            int dirIndex = Random.Range(0, directions.Count);

            // 同時に、進める方向をデータに
            switch (directions[dirIndex])
            {
                case Dirction.Down:
                    mapData[x, z].CanMove[(int)Dirction.Down] = true;
                    SetPath(x, --z);
                    mapData[x, z].CanMove[(int)Dirction.Up] = true;

                    mapData[x, z].CanMove[(int)Dirction.Down] = true;
                    SetPath(x, --z);
                    mapData[x, z].CanMove[(int)Dirction.Up] = true;

                    break;

                case Dirction.Right:
                    mapData[x, z].CanMove[(int)Dirction.Right] = true;
                    SetPath(++x, z);
                    mapData[x, z].CanMove[(int)Dirction.Left] = true;

                    mapData[x, z].CanMove[(int)Dirction.Right] = true;
                    SetPath(++x, z);
                    mapData[x, z].CanMove[(int)Dirction.Left] = true;

                    break;

                case Dirction.Up:
                    mapData[x, z].CanMove[(int)Dirction.Up] = true;
                    SetPath(x, ++z);
                    mapData[x, z].CanMove[(int)Dirction.Down] = true;

                    mapData[x, z].CanMove[(int)Dirction.Up] = true;
                    SetPath(x, ++z);
                    mapData[x, z].CanMove[(int)Dirction.Down] = true;

                    break;

                case Dirction.Left:
                    mapData[x, z].CanMove[(int)Dirction.Left] = true;
                    SetPath(--x, z);
                    mapData[x, z].CanMove[(int)Dirction.Right] = true;

                    mapData[x, z].CanMove[(int)Dirction.Left] = true;
                    SetPath(--x, z);
                    mapData[x, z].CanMove[(int)Dirction.Right] = true;

                    break;
            }
        }

        Vector2Int pathCell = GetStartCell();

        // 掘り進められる方向がまだあればさらに枝分かれ
        if(pathCell.x != -1 && pathCell.y != -1)
        {
            Dig(pathCell.x,pathCell.y);
        }
    }

    /// <summary>
    /// 通路データを生成
    /// 奇数の位置だったら次の生成位置を格納
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    private void SetPath(int x, int z)
    {
        mapData[x, z].ChunkIndex = Path;
        
        if (x % 2 == 1 && z % 2 == 1)
        {
            StartCells.Add(new Vector2Int(x, z));
        }
    }

    /// <summary>
    /// 掘り進めることができなかった場合次に掘り進める位置をランダムに決める
    /// </summary>
    /// <returns>次の掘り進めるスタート位置。なければ(-1,-1)を返す</returns>
    private Vector2Int GetStartCell()
    {
        if (StartCells.Count == 0) return new Vector2Int(-1,-1);

        int index = Random.Range(0, StartCells.Count);

        Vector2Int cell = StartCells[index];
        StartCells.RemoveAt(index);

        return cell;
    }

    /// <summary>
    /// 生成したマップデータをもとにチャンクを生成。
    /// 通路の場合、進めるデータをチャンクに適用
    /// </summary>
    private void CreateStage()
    {
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                if(mapData[i, j].ChunkIndex == OutSideWall)
                {
                    GameObject instans = Instantiate(Chunks[0], new Vector3(10 * i, 0, 10 * j), Quaternion.identity);
                    instans.GetComponent<WallController>().SetWall(mapData[i, j].CanMove);
                }
                else if (mapData[i,j].ChunkIndex == Path)
                {
                    if(Random.Range(0, 2) == 0)
                    {
                        GameObject road = Instantiate(Chunks[0], new Vector3(10 * i, 0, 10 * j), Quaternion.identity);
                        road.GetComponent<WallController>().SetWall(mapData[i, j].CanMove);
                    }
                    else
                    {
                        Instantiate(Chunks[Random.Range(1,Chunks.Length)], new Vector3(10 * i, 0, 10 * j), Quaternion.identity);
                    }
                }
                else
                {
                    Instantiate(Chunks[Random.Range(1, Chunks.Length)], new Vector3(10 * i, 0, 10 * j), Quaternion.identity);
                }
            }
        }
    }

    /// <summary>
    /// デバッグ用のデータ視覚化メソッド
    /// </summary>
    private void DebugData()
    {
        string debugData = "";
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                debugData += mapData[i, j].ChunkIndex;
            }
            debugData += "\n";
        }

        Debug.Log(debugData);
    }
}
