using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class bullet : MonoBehaviour
{

    public GameObject player_Poss;
    public GameObject enemy_Poss;

    float t = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //生成された玉の処理
        if (enemy_Poss != null)
        {
            player_Poss = gameObject;

            transform.position = Vector3.Lerp(player_Poss.transform.position, enemy_Poss.transform.position, t);
            t += Time.deltaTime;

            //時間を利用した射程距離
            Destroy(gameObject,0.4f);
        }

    }

    //敵に接触した時、玉を消す
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
