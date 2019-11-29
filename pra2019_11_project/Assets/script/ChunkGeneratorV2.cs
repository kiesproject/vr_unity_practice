using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneratorV2 : MonoBehaviour
{
    public struct ChunkData
    {
        public int ChunkIndex;
        public bool[] CanMove;

        public void ResetMoveData()
        {
            for (int k = 0; k < CanMove.Length; k++)
            {
                CanMove[k] = false;
            }
        }
    }

    const int Room = 0;
    const int Path = 1;
    const int OutSideWall = 2;
    
    private enum Dirction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    private List<Vector2Int> StartCells;

    [SerializeField] GameObject[] Chunks;

    [SerializeField] int MapX = 6, MapZ = 4;

    public ChunkData[,] mapData;

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

        Dig(1, 1);

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

                if(i == 1 && j == 1)
                {
                    mapData[i, j].CanMove = new bool[] { true, true, true, true };
                }
            }
        }
    }

    private void Dig(int x, int z)
    {
        while (true)
        {
            List<Dirction> directions = new List<Dirction>();

            if (mapData[x, z - 1].ChunkIndex == Room && mapData[x, z - 2].ChunkIndex == Room)
                directions.Add(Dirction.Down);

            if (mapData[x + 1, z].ChunkIndex == Room && mapData[x + 2, z].ChunkIndex == Room)
                directions.Add(Dirction.Right);

            if (mapData[x, z + 1].ChunkIndex == Room && mapData[x, z + 2].ChunkIndex == Room)
                directions.Add(Dirction.Up);

            if (mapData[x - 1, z].ChunkIndex == Room && mapData[x - 2, z].ChunkIndex == Room)
                directions.Add(Dirction.Left);

            if (directions.Count == 0) break;

            SetPath(x, z);

            int dirIndex = Random.Range(0, directions.Count);

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
        if(pathCell.x != -1 && pathCell.y != -1)
        {
            Dig(pathCell.x,pathCell.y);
        }
    }

    private void SetPath(int x, int z)
    {
        mapData[x, z].ChunkIndex = Path;
        
        if (x % 2 == 1 && z % 2 == 1)
        {
            StartCells.Add(new Vector2Int(x, z));
        }
    }

    private Vector2Int GetStartCell()
    {
        if (StartCells.Count == 0) return new Vector2Int(-1,-1);

        int index = Random.Range(0, StartCells.Count);

        Vector2Int cell = StartCells[index];
        StartCells.RemoveAt(index);

        return cell;
    }

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
