using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    //*** =====================================================================================
    //*** [改善]以下のStartGame()というメソッドをButtonのinspectorで設定する必要があります。
    //*** =====================================================================================

    //　スタートボタンを押したら実行する
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}