using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    // ゴール時に表示するText
    public GameObject goalText;

    // トリガーに接触した瞬間に呼ばれる処理
    void OnTriggerEnter(Collider collider)
    {
        // 接触したのがプレイヤーか？
        if (collider.gameObject.CompareTag("Player"))
        {
            // ゴールを表示
            goalText.SetActive(true);
        }
    }
}
