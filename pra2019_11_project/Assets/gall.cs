using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gall : MonoBehaviour
{
    //***クラスの名前は大文字で始まることが多いです。これは一目でクラスと判別するためです。
	//***このルールは実際の開発などでプログラミングのセオリーとなっています。今後はクラスは大文字にする癖をつけると良いでしょう。


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.z > 10)
        {
            //***旧式の命令はあまり使わない方がいいですね
            Application.LoadLevel("Stage1");
        }
    }
}
