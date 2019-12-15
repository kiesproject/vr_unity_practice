using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonLoader : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public void OnClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
