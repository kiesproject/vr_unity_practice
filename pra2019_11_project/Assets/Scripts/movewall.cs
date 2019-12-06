using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movewall : MonoBehaviour
{
    //*** ==================
    //*** 良いです。
    //*** ==================

    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);//回転する
    }
}
