using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereCol : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            SceneManager.LoadScene("ClearScene");
        }
    }
}
