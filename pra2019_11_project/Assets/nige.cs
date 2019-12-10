// Player.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// プレイヤー
public class nige : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    void Update()
    {
        // 左に移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-0.2f, 0.0f, 0.0f);
        }
        // 右に移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(0.2f, 0.0f, 0.0f);
        }
        // 前に移動
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(0.0f, 0.0f, 0.2f);
        }
        // 後ろに移動
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(0.0f, 0.0f, -0.2f);
        }
        
    }
    void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name == "oni")
            {
                SceneManager.LoadScene("GAME OVER");
            }
            if (collision.gameObject.name == "Goal")
            {
                SceneManager.LoadScene("Clear");
            }
    }
}
