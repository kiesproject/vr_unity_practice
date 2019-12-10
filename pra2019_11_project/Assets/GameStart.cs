using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    //*** ==================================================================================
    //*** そのとおり！クラスの枠を超えてメソッドを実行させたい場合はpublicが必要になります。
    //*** ==================================================================================

    // 先頭に「public」を必ずつけること（ポイント）
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("Main");
    }
}