using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGeneratorV2 : MonoBehaviour
{
    public struct ChunkData
    {
        public int ChunkIndex;
        public bool[] CanMove;

        public int GroupIndex;

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
    }

    private void ResetMapData()
    {
        mapData = new ChunkData[MapX, MapZ];
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                mapData[i, j].ChunkIndex = 0;
                mapData[i, j].GroupIndex = 0;
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

    private void CreateStage()
    {
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                GameObject obj = Instantiate(Chunks[mapData[i, j].ChunkIndex], new Vector3(10 * i, 0, 10 * j), Quaternion.identity);
                obj.GetComponent<GroupChecker>().CheckGroups(mapData[i, j].GroupIndex);
            }
        }
    }
}
