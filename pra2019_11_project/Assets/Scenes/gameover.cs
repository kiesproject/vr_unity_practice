
using UnityEngine;
using System.Collections;

public class atarihantei : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        //*** ========================================================================================================================================
        //*** [改善]このscriptをプレイヤーに付けるのなら、敵に"Enemy"タグをつけて下のif文を(collision.gameObject.tag == "Enemy")にするといいでしょう。
        //***       敵に付けるならプレイヤーに"Player"タグを付けて(collision.gameObject.tag == "Player")で判定すると良いでしょう。
        //*** ========================================================================================================================================

        if (collision.gameObject.name == "Gameover")
        {
            //*** ===============================================================================================================
            //*** [改善] Application.LoadLevelは古い書き方なので使わない方がいいでしょう。
            //***        現在はSceneManager.LoadScene();を使います。一番最初にusing UnityEngine.SceneManagement;を書きましょう。
            //*** ===============================================================================================================

            Application.LoadLevel("Gameover");
        }
    }
}