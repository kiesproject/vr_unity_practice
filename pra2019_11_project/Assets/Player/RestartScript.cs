using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RestartScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.y < -10)
        {
            //*** ==============================================================================================================================
            //*** [アドバイス]なるべく古い書き方は使わない方がいいかもしれないです。
            //*** 現在はSceneManager.LoadScene()が推奨されています。最初にusing UnityEngine.SceneManagement;を書くのを忘れないようにしてくださいね
            //*** ==============================================================================================================================

            Application.LoadLevel(0);
        }

        //もし空中でジャンプするとリスタート
       // Vector3 pos = transform.position;
        //if ()
        //{
          //  Application.LoadLevel(0);
        //}
    }
}
