using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear : MonoBehaviour
{
    public GameObject juge;  //クリア判定

    public void OnTriggerEnter(Collider other)
    {
        //クリア条件を満たしている場合プレイヤーが接触したときにこのゲームオブジェクトを消去してクリア判定をアクティブにする

        if (controler.instance.clear == true)  
        {
            if(other.tag == "Finish")
            {
                juge.SetActive(true);
                Destroy(this.gameObject);
            }
        }
    }
}
