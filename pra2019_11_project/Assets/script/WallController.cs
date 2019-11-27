using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] GameObject[] walls = new GameObject[4];

    /// <summary>
    /// 壁の出現する方向を指定
    /// </summary>
    /// <param name="index">出現させる位置</param>
    /// <returns>既に出現している壁の数</returns>
    public void SetWall(bool[] data)
    {
        for(int i = 0; i < walls.Length; i++) walls[i].SetActive(!data[i]);
    }
}
