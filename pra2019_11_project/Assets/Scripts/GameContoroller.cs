using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameContoroller : MonoBehaviour
{
    /// <summary>
    /// ゲーム全体の時間やUI
    /// 敵や目的地の生成などを行う。
    /// 
    /// 呼び出せる関数としてゲームオーバーメソッドとタイム追加メソッドがある。
    /// 
    /// ★・・・反省点
    /// ☆・・・重要なポイント
    /// </summary>
    /// 
    /// 
    /// 
    /// ここでは使用するスクリプトや変数の宣言をしている
    /// 

    [SerializeField] float time;
    [SerializeField] float borninterval;
    [SerializeField] float score;
    public GameObject Enemy;
    public GameObject Player;
    public GameObject Warning;
    public GameObject Destination;
    private PlayerScript playerScript;
    public Text timetext, scoretext, jumpcounttext,gameovertext,gameovertext2,result;
    public Image blackbackGround;
    private float x;
    private float z;
    private string name;

    private void Start()
    {
        //playerscriptからjumpcountを得るためにcomponentを取得
        playerScript= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        //timeとscoreのテキスト
        timetext.text = "Time:"+time.ToString();

        scoretext.text = "Score:" + scoretext.ToString();
        //先ほど取得したplayerscriptのjumpcountを使う
        jumpcounttext.text = "jump残り:" + playerScript.jumpcount.ToString();

        //ゲームオーバー用の文字などを取得しenabledにしておく

        //★…子オブジェクトにしたらテキスト説明が一緒に消えると思ったが消えなかったので仕方なく2個同時に操作している
        gameovertext = GameObject.Find("GameOver").GetComponent<Text>();
        gameovertext2 = GameObject.Find("GameOver2").GetComponent<Text>();
        gameovertext.enabled = false;
        gameovertext2.enabled = false;
        blackbackGround = GameObject.Find("BlackBackGround").GetComponent<Image>();
        result = GameObject.Find("Result").GetComponent<Text>();

        //☆重要な場所1 ここで敵を生成している
        //コルーチンでEnemyを生成するメソッドをborninterval秒ごとに呼び出している
        StartCoroutine(EnemyCreate(borninterval));

    }

    private void Update()
    {
        //ここで時間を経過＆ボードを更新している
        //もし時間が5以下ならtimeを赤色に
        time -= 1f*Time.deltaTime;
        if (time < 6)
        {
            timetext.text = "Time:" + ((int)time).ToString();
            //RGBを255fで割ると色を指定できる、この場合は赤
            timetext.color = new Color(200f / 255f, 0f/ 255f, 0f / 255f);
        }
        else
        {
            timetext.text = "Time:" + ((int)time).ToString();
            //黒色
            timetext.color = new Color(0f / 255f, 0f / 255f, 0f / 255f);
        }
       
        //スコア更新
        scoretext.text = "Score:" + score.ToString();
        //jump回数更新
        jumpcounttext.text = "jump残り:" + playerScript.jumpcount.ToString();

        //もしtag(destination)がないならdestinationを生成
        GameObject des = GameObject.FindWithTag("Destination");
        if (des == null)
        {
            //x,zをランダムに決定しprefaを召喚、
            //★同じような処理が多いのでメソッド化すればよかった
            x = Random.Range(-5f, 5f);
            z = Random.Range(-5f, 5f);
            Vector3 post= new Vector3(x, 5f, z);
            Instantiate(Destination,post, Quaternion.identity);
        }

        //ここから下はゲームオーバー関連

        if (time <= 0)
        {
            //時間経過によるゲームオーバー

            gameover();
        }

        if (Player.transform.position.y < -2)
        {
            //フィールドから落ちた
            gameover();
        }

    }

    //playerから呼び出される
    public void timeadd()
    {
        //Destinationに触れた場合呼び出される
        time += 3;
        score += 1;
    }

    void gameover()
    {
        //まず画面を暗くする
        blackbackGround.enabled = true;
        //start()で取得してあるのでこのまま使える,ゲームオーバーの表示
        gameovertext.enabled = true;
        gameovertext2.enabled = true;
        //ゲーム中の時間を止める、これでプレイヤーや制限時間が進まない
        Time.timeScale = 0;
        ///
        /// スコアで称号が変化
        ///
        if (score < 5)
        {
            name = "ミジンコ";
        }
        else if (score < 10)
        {
            name = "一般人";
        }
        else if (score < 15)
        {
            name = "地方芸人";
        }
        else if (score < 20)
        {
            name = "テレビに出てる人";
        }
        else if (score < 25)
        {
            name = "みのもんだ";
        }
        else if (score < 30)
        {
            name = "タピオカ";

        } 
        else if (30<=score)
        {
            name = "GACUKT";
        }

        result.enabled = true;
        result.text = "あなたは・・・\n" + name+"級です";


        //spaceキーでtitleへ
        if (Input.GetKeyDown("space"))
        {
            //画面を暗くするのとresult表示を解除
            blackbackGround.enabled = false;
            result.enabled = false;
            //timescaleを戻しておく
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("title");
        }

    }


    //コルーチン、敵の場所がある程度わかるように時間経過で出現
    private IEnumerator EnemyCreate(float waittime)
    {
        while (true)
        {
            //★同じような処理
            x = Random.Range(-5f, 5f);
            z = Random.Range(-5f, 5f);
            Vector3 pos = new Vector3(x, 0.5f, z);

            //5秒待つ
            yield return new WaitForSeconds(waittime);
            //出現場所から紫色エフェクトが発生
            Instantiate(Warning, pos, Quaternion.identity);
            //コルーチンで1秒後に敵召喚,ウェイトタイムと出現場所を入力
            yield return new WaitForSeconds(1);
            Instantiate(Enemy, pos, Quaternion.identity);

            //一回ごとにwaittimeを-0.2f,ただし1.5fより小さくはならない
            if (waittime > 1.5f)
            {
                waittime -= 0.5f;
            }
        }
    }
}
