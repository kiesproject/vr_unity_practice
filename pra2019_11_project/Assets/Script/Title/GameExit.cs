using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void OnClick()
    {
        Application.Quit();
    }
}
