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

    }

    private void Dig(int x, int z)
    {
        Random rand = new Random();


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
