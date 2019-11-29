using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))//ゲームシーンへ遷移
        {
            SceneManager.LoadScene("Maze");
            // Debug.Log("OK");
        }
    }
}
