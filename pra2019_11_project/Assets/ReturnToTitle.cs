using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToTitle : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public void OnButtonClicked()
    {
        SceneManager.LoadScene("start");
    }
}
