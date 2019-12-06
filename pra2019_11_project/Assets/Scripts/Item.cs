using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //*** ==================
    //*** 良いです。
    //*** ==================

    void Update()
    {
        //*** アニメーションを使うのも手ですね
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);//斜めに回転する
    }

    private void OnTriggerEnter(Collider other)//プレイヤーにあったったら消滅する
    {
        if(other.tag == "Finish")
        {
            Destroy(this.gameObject);
        }
    }
}
