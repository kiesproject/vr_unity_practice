using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalManager: MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public GameObject final_object = null;
    public float timecount_final;
    //playerに付いているHitplayerというscriptを取得
    [SerializeField] private HitPlayer anotherScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //*** =============================================================================================
        //*** [アドバイス]Update内でGetComponentを使うのはちょっと重くなるのでStart()に書いた方がいいです。
        //*** =============================================================================================

        timecount_final -= Time.deltaTime;
        //playerが非表示になったらYOU LOSEを表示
        if (anotherScript.fin)
        {
            Text final_text = final_object.GetComponent<Text>();
            final_text.text = "YOU LOSE";
        }

        //20秒耐えきったらYOU WINを表示
        else if (timecount_final <= 0)
        {
            Text final_text = final_object.GetComponent<Text>();
            final_text.text = "YOU WIN !!";
        }
    }
}
