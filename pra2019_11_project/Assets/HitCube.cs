using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    [SerializeField] private HitPlayer anotherScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //cubeに当たったobjectのtagがenemyの場合、それを消去する
    //cubeに当たったobjectのtagがplayerの場合、それを非表示にする
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("plane") == false)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("player"))
            {
                collision.gameObject.SetActive(false);
                anotherScript.fin = true;
            }
        }
    }
}
