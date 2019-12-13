using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCube : MonoBehaviour
{
    [SerializeField] private HitPlayer anotherScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    //cubeに当たったobjectのtagがenemyの場合、それを消去する
    //cubeに当たったobjectのtagがplayerの場合、それを非表示にする
    void OnCollisionEnter(Collision collision)
    {
        //*** ============================================================================================================
        //*** [アドバイス] ! を付けるとtrueとfalseが逆になります。
        //*** つまり、!collision.gameObject.CompareTag("plane") とすると、planeタグが検出されないときにtrueになります。
        //*** ============================================================================================================


        if (collision.gameObject.CompareTag("plane") == false)
        {
            if (collision.gameObject.CompareTag("enemy"))
            {
                Destroy(collision.gameObject);
            }

            //*** =============================================
            //*** [改善]実際に登録されているタグはPlayerです。
            //*** =============================================

            //else if (collision.gameObject.CompareTag("player"))
            else if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.SetActive(false);


                //*** ===================================================================================================
                //*** [改善]ゲームオーバーになるとanotherSctiptがmissingになってしまうためその対策をする必要があります。
                //*** ===================================================================================================

                anotherScript.fin = true;
            }
        }
    }
}
