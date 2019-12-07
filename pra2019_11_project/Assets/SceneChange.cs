using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //*** =========================================================================================================
    //*** [アドバイス]SceneChange2を内容が被っているのでLoadScene()の引数をinspectorで指定させれば使いまわせます。
    //*** =========================================================================================================


    //マウスの左ボタンをクリックすると、シーンが次のものに変わる

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
