using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] GameObject[] walls = new GameObject[4];
    private int wallCount = 0;

    /// <summary>
    /// 壁の出現する方向を指定
    /// </summary>
    /// <param name="index">出現させる位置</param>
    /// <returns>既に出現している壁の数</returns>
    public int SetWall(int index)
    {
        wallCount++;
        walls[index].SetActive(true);

        return wallCount;
    }
}
