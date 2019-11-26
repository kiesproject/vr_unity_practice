using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    /// <summary>
    /// タイトル画面用スクリプト
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene("main");
        }
    }
}
