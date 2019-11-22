﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
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

    [SerializeField] int MapX = 3, MapZ = 3;

    public ChunkData[,] mapData;

    // Start is called before the first frame update
    void Start()
    {
        ResetMapData();

        CreateStageData();

        CreateStage();
    }
    
    private void ResetMapData()
    {
        mapData = new ChunkData[MapX, MapZ];
        for(int i = 0; i < MapX; i++)
        {
            for(int j = 0; j < MapZ; j++)
            {
                mapData[i, j].ChunkIndex = 0;
                mapData[i, j].CanMove = new bool[4];
                mapData[i, j].ResetMoveData();
            }
        }
    }

    private void CreateStageData()
    {
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                int selectedChunk;

                if (i == 0 && j == 0)
                {
                    selectedChunk = Random.Range(1, Chunks.Length);     //スタートは部屋チャンクから
                }
                else
                {
                    selectedChunk = Random.Range(0, Chunks.Length);
                }
                
                Debug.Log(selectedChunk);
                mapData[i, j].ChunkIndex = selectedChunk;
            }
        }
    }

    /// <summary>
    /// 分かれ道を生成
    /// </summary>
    /// <param name="x">生成するチャンクのMapX位置</param>
    /// <param name="z">生成するチャンクのMapZ位置</param>
    private void CreateJanction(int x, int z)
    {
        int count = 0;

        GameObject wall = Instantiate(Chunks[mapData[x, z].ChunkIndex], new Vector3(10 * x, 0, 10 * z), Quaternion.identity);
        if (x == 0)
        {
            count = wall.GetComponent<WallController>().SetWall(3);
        }
        else if (x == (MapX - 1))
        {
            count = wall.GetComponent<WallController>().SetWall(1);
        }

        if (z == 0)
        {
            count = wall.GetComponent<WallController>().SetWall(2);
        }
        else if (z == (MapZ - 1))
        {
            count = wall.GetComponent<WallController>().SetWall(0);
        }

        if (count <= 1)
        {
            wall.GetComponent<WallController>().SetWall(Random.Range(0, 4));
        }
    }

    private void CreateStage()
    {
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                if(mapData[i,j].ChunkIndex == 0)
                {
                    CreateJanction(i, j);
                }
                else
                {
                    Instantiate(Chunks[mapData[i, j].ChunkIndex], new Vector3(10 * i, 0, 10 * j), Quaternion.identity);
                }
            }
        }
    }
}
