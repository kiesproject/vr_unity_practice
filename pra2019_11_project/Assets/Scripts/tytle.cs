﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tytle : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))//エンターかクリックでゲームスタート
        {
            SceneManager.LoadScene("main");
        }
    }
}
