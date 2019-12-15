using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Switch : MonoBehaviour
{
    public GameObject lightObj;
    private void Start()
    {
        StartCoroutine("ImageSwitch");
    }

    //*** ============================================================================================
    //*** [アドバイス]採点者はコルーチンが大好きなので、コルーチンを使っていることを大いに褒めます。素晴らしい。
    //***             でもここの処理は一つにまとめられます。
    //*** ============================================================================================

    IEnumerator ImageSwitch()
    {
        //ここに処理を書く
        lightObj.SetActive(false);
        //1フレーム停止
        yield return new WaitForSeconds(0.1f);

        //ここに再開後の処理を書く
        StartCoroutine("ImageSwitch2");
    }
    IEnumerator ImageSwitch2()
    {
        lightObj.SetActive(true);
        yield return new WaitForSeconds(3);
        StartCoroutine("ImageSwitch");
    }

    //*** ===============================
    //*** 以下の様に書くことも出来ます。

    /*
    IEnumerator ImageSwitch()
    {
        while (true)
        {
            //ここに処理を書く
            lightObj.SetActive(false);
            //1フレーム停止
            yield return new WaitForSeconds(0.1f);

            lightObj.SetActive(true);

            yield return new WaitForSeconds(3);
        }
    }
    */

    //*** ===============================
}
