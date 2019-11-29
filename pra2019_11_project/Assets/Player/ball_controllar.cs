using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_controllar : MonoBehaviour
{



    public float speed = 10;
    public float power = 1000;

    private void FixedUpdate()
    {
        // Rigidbodyを取得
        Rigidbody rigidbody = GetComponent <Rigidbody>();

        // 方向キーの入力
        float x = Input.GetAxis("Horizontal"); // 横軸
        float z = Input.GetAxis("Vertical"); // 縦軸

        // ベクトルに変換
        Vector3 vec = new Vector3(x, 0, z);

        vec *= speed;

        // 力を加える
        rigidbody.AddForce(vec);
    }
    // Update is called once per frame

    // 衝突したときに呼ばれる処理
    private void OnCollisionEnter(Collision collision)
    {
        // Enemyと衝突したか？
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //衝突情報を取得
            ContactPoint contact = collision.contacts[0];

            //プレイヤーを吹き飛ばす処理
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.AddForce(contact.normal * power);
                
        }
    }
}
