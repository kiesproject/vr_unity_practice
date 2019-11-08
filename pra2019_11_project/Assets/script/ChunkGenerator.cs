using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] GameObject[] Chunks;

    public int[,] mapData;

    [SerializeField] int MapX = 3, MapZ = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        ResetMapData();

        CreateStageData();

        CreateStage();
    }
    
    private void ResetMapData()
    {
        mapData = new int[MapX, MapZ];
        for(int i = 0; i < MapX; i++)
        {
            for(int j = 0; j < MapZ; j++)
            {
                mapData[i, j] = 0;
            }
        }
    }

    private void CreateStageData()
    {
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                int selectedChunk = Random.Range(0, Chunks.Length);
                Debug.Log(selectedChunk);
                mapData[i, j] = selectedChunk;
            }
        }
    }

    private void CreateStage()
    {
        for (int i = 0; i < MapX; i++)
        {
            for (int j = 0; j < MapZ; j++)
            {
                Instantiate(Chunks[mapData[i, j]], new Vector3(10 * i, 0, 10 * j), Quaternion.identity);
            }
        }
    }
}
