using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Contllorar : MonoBehaviour
{
    public float moveSpeed = 1.0f;          // 移動速度
    public float moveDuration = 1.0f;       // 動作の継続時間
    public float moveDirection = 0;         // 移動方向　0…左　1…右

    float moveElapsedTime = 0;              // 動作経過時間

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // 更新処理
    void Update()
    {
        // 動作開始からの経過時間を測定
        moveElapsedTime += Time.deltaTime;

        // 継続時間終了
        if (moveElapsedTime >= moveDuration)
        {
            // 移動方向を切り替え
            // 左　→　右
            if (moveDirection == 0)
            {
                moveDirection = 1;
            }
            // 右　→　左
            else if (moveDirection == 1)
            {
                moveDirection = 0;
            }

            // 経過時間をリセット
            moveElapsedTime = 0;
        }


        // 左に移動
        if (moveDirection == 0)
        {
            this.transform.position += new Vector3(-moveSpeed, 0, 0) * Time.deltaTime;
        }
        // 右に移動
        else
        {
            this.transform.position += new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }
    }
}
