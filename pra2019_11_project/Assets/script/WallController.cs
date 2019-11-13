using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] GameObject[] walls = new GameObject[4];

    /// <summary>
    /// 出現させる壁を指定
    /// </summary>
    /// <param name="index">出現させる位置</param>
    public void SetWall(int index)
    {
        walls[index].SetActive(true);
    }
}
