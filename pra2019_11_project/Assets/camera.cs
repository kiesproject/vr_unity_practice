using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    Vector3 def;

    void Awake()
    {
        def = transform.localRotation.eulerAngles;
    }

    void Update()
    {
        Vector3 _parent = transform.parent.transform.localRotation.eulerAngles;

        //修正箇所
        transform.localRotation = Quaternion.Euler(def - _parent);

        //ログ用
        Vector3 result = transform.localRotation.eulerAngles;
        Debug.Log("def=" + def + "     _parent=" + _parent + "     result=" + result);

        //*** ===============================================================================================
        //*** [アドバイス]Debug.Log()で値がどのようになっていうのかを確認するのはグッジョブですね。
        //***             上のように一行で複数の変数を覗きたい場合におすすめしたいのはstring.Format()です。
        //***             pythonにも似たような命令があるとおもいます。良ければ調べて見てください。
        //*** ===============================================================================================

    }


}