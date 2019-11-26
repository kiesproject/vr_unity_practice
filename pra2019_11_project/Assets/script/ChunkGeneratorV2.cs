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
                CanMove[k] = true;
            }
        }
    }
    
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

    // Start is called before the first frame update
    void Start()
    {
        ResetMapData();

        CreateStageData();

        CreateStage();

        DebugData();
    }

    private void ResetMapData()
    {
        mapData = new ChunkData[MapX, MapZ];
        StartCells = new List<Vector2Int>();
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                mapData[i, j].ChunkIndex = 0;

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
                    mapData[i, j].ChunkIndex = 1;
                }
                else
                {
                    mapData[i, j].ChunkIndex = 0;
                }
            }
        }

        Dig(1, 1);

        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                if (i == 0 || j == 0 || i == MapX - 1 || j == MapZ - 1)
                {
                    mapData[i, j].ChunkIndex = 0;
                }
            }
        }
    }

    private void Dig(int x, int z)
    {
        while (true)
        {
            List<Dirction> directions = new List<Dirction>();

            if (mapData[x, z - 1].ChunkIndex == 0 && mapData[x, z - 2].ChunkIndex == 0)
                directions.Add(Dirction.Down);

            if (mapData[x + 1, z].ChunkIndex == 0 && mapData[x + 2, z].ChunkIndex == 0)
                directions.Add(Dirction.Right);

            if (mapData[x, z + 1].ChunkIndex == 0 && mapData[x, z + 2].ChunkIndex == 0)
                directions.Add(Dirction.Up);

            if (mapData[x - 1, z].ChunkIndex == 0 && mapData[x -2, z].ChunkIndex == 0)
                directions.Add(Dirction.Left);

            if (directions.Count == 0) break;

            SetPath(x, z);

            int dirIndex = Random.Range(0, directions.Count);

            switch (directions[dirIndex])
            {
                case Dirction.Down:
                    SetPath(x, --z);
                    SetPath(x, --z);
                    break;

                case Dirction.Right:
                    SetPath(++x, z);
                    SetPath(++x, z);
                    break;

                case Dirction.Up:
                    SetPath(x, ++z);
                    SetPath(x, ++z);
                    break;

                case Dirction.Left:
                    SetPath(--x, z);
                    SetPath(--x, z);
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
        mapData[x, z].ChunkIndex = 1;
        if(x % 2 == 1 && z % 2 == 1)
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
                Instantiate(Chunks[mapData[i, j].ChunkIndex], new Vector3(10 * i, 0, 10 * j), Quaternion.identity);
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
