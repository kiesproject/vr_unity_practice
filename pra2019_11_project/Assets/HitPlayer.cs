using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPlayer: MonoBehaviour
{
    public bool fin;

    // Start is called before the first frame update
    void Start()
    {
        fin = false;
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        //*** =============================================================================================
        //*** bool型に比較演算子を使用できます。
        //*** (条件式1) || (条件式2)　を使用すると条件式1と条件式2どちらかが当てはまったらtrueを返します。
        //*** つまり、以下の様に書くことが出来ます。
        /*
        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("wall"))
        {
            gameObject.SetActive(false);
            fin = true;
        }
        */
        //*** =============================================================================================

        //tagがenemyかwallのobjectに当たった場合、自身を非表示にする
        if (collision.gameObject.CompareTag("enemy"))
        {
            gameObject.SetActive(false);
            fin = true;
        }
        else if (collision.gameObject.CompareTag("wall"))
        {
            gameObject.SetActive(false);
            fin = true;
        }

    }
}
