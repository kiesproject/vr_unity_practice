using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{
    string filePath;
    SaveData save;

    public Text GameScore;
    public Text HScore;

    public GameObject GameOver_A;
    public GameObject[] randomEnemy;

    [HideInInspector]
    public int score = 0; //取得したスコア
    private int Hscore = 0; //ハイスコア
    [HideInInspector]
    public static bool RScore = false;

    private int number = 0;
    private float time = 0.0f;
    public float appearTime = 1.0f;

    float x = 0;
    float y = 0;
    float z = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*マウスカーソルをゲームウィンドウから出さない
        マウスカーソルが見えなくなるが、Escキーを押すと出てくる*/
        Cursor.lockState = CursorLockMode.Locked;

        filePath = Application.dataPath + "/" + ".savedata.json";
        save = new SaveData();

        //前回までのハイスコアのロード
        Load();
        Hscore = save.Hscore_S;

        //ハイスコアのリセット
        if(RScore == true)
        {
            save.Hscore_S = 0;
            Save();
            Hscore = 0;
            RScore = false;
        }

        HScore.text = Hscore.ToString();

        //スコアの初期化
        GameScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //スコアの代入
        GameScore.text = score.ToString();

        //*** =======================================================================================================================
        //*** [改善]ファイルの読み書きは速度的には遅いので、毎フレーム実行しない方がいいです。つまりUpdate内に書かない方が良いですね
        //***       今回の場合は、ゲームオーバーになったらハイスコアを書きこむ方がいいです。
        //*** =======================================================================================================================

        //ハイスコアの判定と記録
        if(score > Hscore)
        {
            save.Hscore_S = score;
            Save();
            HScore.text = save.Hscore_S.ToString();
        }

        time += Time.deltaTime;

        //敵のランダム出現
        if (time > appearTime)
        {
            //*** =====================================================================================================================================================
            //*** [アドバイス]値型の変数x,y,zは敵のランダム配置でしか使われていないので、このブロック内に宣言しておくとメモリの節約になります。
            //***
            //***             少し詳しく言うと、クラス変数として宣言するとヒープ領域にメモリが確保され、ローカル変数として宣言するとスタック領域にメモリが確保されます。
            //***             スタック領域は使い終わったらメモリを開放しますが、ヒープ領域はガベージコレクションが実行されるまで解放されません。
            //***             詳しくは情報工学科の講義でやりますし、勉強会でもやるかも…？
            //***
            //***             とどのつまり、x, y, zはローカル変数として宣言した方がいいよ、といいうことです。まあ、そこまで影響は無いと思うので気にしなくてもいいかも？
            //*** =====================================================================================================================================================

            x = Random.Range(-9.0f, 9.0f);
            y = Random.Range(1.0f, 2.0f);
            z = Random.Range(-9.0f, 9.0f);

            number = Random.Range(0, randomEnemy.Length);
            Instantiate(randomEnemy[number], new Vector3(x, y, z), transform.rotation);

            time = 0.0f;
        }
    }

    //セーブデータ
    [System.Serializable]
    public class SaveData
    {
        public int Hscore_S = 0;
    }

    //*** ========================================
    //*** さすがだぞ！　jsonの読み書きの　仕方を　
    //*** ばっちり　わかって　いるんだな！！
    //*** ========================================

    //セーブ処理
    public void Save()
    {
        string json = JsonUtility.ToJson(save);

        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

    //ロード処理
    public void Load()
    {
        if (File.Exists(filePath))
        {
            StreamReader streamReader;
            streamReader = new StreamReader(filePath);
            string data = streamReader.ReadToEnd();
            streamReader.Close();

            save = JsonUtility.FromJson<SaveData>(data);
        }
    }
}
