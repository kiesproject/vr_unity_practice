using UnityEngine;
using System.Collections;

public class SearchScript : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    private EnemyScript enemyscript;
    /// <summary>
    /// Enemyの子オブジェクトのsearchareaに付いているスクリプト
    /// 
    /// </summary>
    private void Start()
    {
        //ここで親オブジェクトのEnemyScriptを取得
        enemyscript = GetComponentInParent<EnemyScript>();
    }

    //Playerのタグがあるものに当たったら親スクリプトのchaseメソッドを実行
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            enemyscript.Chase();
        }

        

    }
    
    //Playerタグが範囲から出たらstayする
    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            enemyscript.Stay();
        }
    }

}