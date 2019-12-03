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
}
