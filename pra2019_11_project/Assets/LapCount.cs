using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCount : MonoBehaviour
{
    int counter = 0;
    public GameObject LapCounter;

    //*** ==============================================================================================================================================
    //*** [改善]衝突判定を行うOnCollisionEnter(Collosion)はMonoBehaviourで定義されているメソッドなのでクラスメソッドとして書かなければなりません。
    //***       つまり、OnCollisionEnter(Collosion)を書く場所が違います。上手く動かなかったのでそれが原因です。
    //*** ==============================================================================================================================================

    void Update()
    {
        /*
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "stageCollider")
            {
                counter += 1;
            }

            if (this.counter % 6 == 0)
            {
                this.LapCounter.GetComponent<Text>().text = counter.ToString("D1") + "/" + "6";
            }
        }
        */
    }

    //*** =================================================================================================================
    //*** [改善]onTriggerにチェックが入っているコリダーとの接触判定を定義する場合はOnTriggerEnter(Collider)を使用します。
    //*** =================================================================================================================

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "stageCollider")
        {
            counter += 1;
        }

        //*** ============================================================================================================
        //*** 4をかけている理由ですが、親オブジェクトにRigidbodyが付いている場合は子オブジェクトのコリダーも検知します。
        //*** 今回の場合は一回通過するたびにcouterが+4されるので*4しています。
        //*** ============================================================================================================

        if (this.counter % (6 * 4) == 0)
        {
            this.LapCounter.GetComponent<Text>().text = (counter / (6 * 4)).ToString("D1") + "/" + "6";
        }
    }
}
