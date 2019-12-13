using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject start_object = null;
    public float timecount_start;

    bool flag = false;

    // Start is called before the first frame update
    void Start()
    {
        timecount_start = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
        //*** ====================================================================================
        //*** [改善]Destroyでゲームオブジェクトを消去した後参照するとmissingでエラーが起きます。
        //***       削除されたら実行しないように工夫する必要があります。
        //*** ====================================================================================

        //*** 一例↓ flagという変数を作ってflagが立ったら処理をしないようにする
        if (!flag)
        {
            //ゲーム開始時のUIの表示
            Text start_text = start_object.GetComponent<Text>();
            start_text.text = "GAME START !!";
            timecount_start -= Time.deltaTime;
            if (timecount_start <= 0)
            {
                Destroy(start_object);
                flag = true;
            }
        }
    }
}