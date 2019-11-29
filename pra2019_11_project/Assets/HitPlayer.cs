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
