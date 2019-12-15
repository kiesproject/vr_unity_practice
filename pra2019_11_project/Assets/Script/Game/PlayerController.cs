using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float inputHorizontal;
    float inputVertical;
    Rigidbody rb;

    //*** =============================================================================================================================
    //*** [アドバイス]MonoBehaviourの中にすでにCamera cameraが宣言されています。
    //***             そっちのcameraを使わない限り問題無いとは思いますが、混乱を避けるためにあんまり被る名前は使わない方がいいでしょう。
    //*** =============================================================================================================================

    public Camera camera;

    float moveSpeed = 3f;
    public float speed = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputHorizontal = Input.GetAxisRaw("Horizontal")*speed;
        inputVertical = Input.GetAxisRaw("Vertical")*speed;
        rb.AddForce(inputHorizontal,0,inputVertical);

        //*** =================================
        //*** [改善]AddForceは必要ないと思います。
        //*** =================================

    }

    void FixedUpdate()
    {
        //*** ====================================
        //*** カメラを配慮した移動は素晴らしいです
        //*** ====================================

        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * inputVertical + camera.transform.right * inputHorizontal;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        rb.velocity = moveForward * moveSpeed + new Vector3(0, rb.velocity.y, 0);

        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }
}