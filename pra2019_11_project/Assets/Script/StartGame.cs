using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    //スタートボタンの処理
    public void GameStart()
    {
        SceneManager.LoadScene("Game");
    }

    //スコアリセットボタンの処理
    public void ResetScore()
    {
        GameManager.RScore = true;
    }
}