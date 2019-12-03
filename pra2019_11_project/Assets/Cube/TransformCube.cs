using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformCube : MonoBehaviour
{
    //*** ===============================================================
    //*** [改善]これらの変数はVector3型でまとめると可読性が上がるでしょう
    //***↓
    //*** ===============================================================

    public float posx1 = 0f;
    public float posy1 = 0f;
    public float posz1 = 0f;
    public float posx2 = 0f;
    public float posy2 = 0f;
    public float posz2 = 0f;
    //地面に接触しているか否か
    bool Ground = true;
    int key = 0;

    // Start is called before the first frame update
    void Start()
    {
        int a = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if(Input.GetKeyDown(KeyCode.Space))
        {

            //*** ===================================================================================================
            //*** [改善]このままではif(Ground == true)の必要はないです。 発表でSetActive()を使えばよかったというのはここですかね？
            //***       GameObject.SetActive()を使うとまとまって良いと思います。
            //*** ===================================================================================================

            // プレイヤーがどこかに着地しているか？
            if (Ground == true)
            {
                // 床が見えるところにあるか？
                if (pos.y > -5)
                {
                    transform.position = (new Vector3(posx1, posy1, posz1));
                }
                else
                {
                    transform.position = (new Vector3(posx2, posy2, posz2));
                    
                }
               // Ground = false;
               // Invoke("reset", 1);

                //void reset()
                //{
                  //  Ground = true;
                //}

            }
           
        }
    }

   
}
