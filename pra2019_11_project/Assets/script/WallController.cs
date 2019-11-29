using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] GameObject[] walls = new GameObject[4];

    /// <summary>
    /// 通行可能な方向データをもとに壁を有効化
    /// </summary>
    /// <param name="data">通行可能な方向データ</param>
    public void SetWall(bool[] data)
    {
        for(int i = 0; i < walls.Length; i++) walls[i].SetActive(!data[i]);
    }
}
