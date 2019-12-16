using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UnityEngine.UI.Text scoreLabel;
    public GameObject winnerLabelObject;
    public GameObject scoreLabelObject;
    public GameObject timeLabelObject;

    public void Update()
    {
        //====================================================================================================================
        //*** [改善] GameObject.Find()などのFind系の命令は実行時間が長いのでUpdate()内に書いてしまうと重くなる原因になります。
        //***        この場合は、フラグを使うなどした方がいいかもしれないですね。
        //====================================================================================================================

        int count = GameObject.FindGameObjectsWithTag("Item").Length;
        scoreLabel.text = count.ToString();

        if (count == 0)
        {
            winnerLabelObject.SetActive(true);
            scoreLabelObject.SetActive(false);
            timeLabelObject.SetActive(false);
        }

    }

}