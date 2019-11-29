using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // 先頭に「public」を必ずつけること（ポイント）
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}