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

    [HideInInspector]
    public int score = 0;
    private int Hscore = 0; //ハイスコア

    private int number = 0;
    private float time = 0.0f;
    public float appearTime = 1.0f;

    float x = 0;
    float y = 0;
    float z = 0;

    public GameObject[] randomEnemy;


    // Start is called before the first frame update
    void Start()
    {
        //マウスカーソルをウィンドウから出さない
        //マウスカーソルが見えなくなるが、Escキーで出現する
        Cursor.lockState = CursorLockMode.Locked;

        filePath = Application.dataPath + "/" + ".savedata.json";
        save = new SaveData();

        Load();
        Hscore = save.Hscore_S;
        HScore.text = Hscore.ToString();
        //スコアの初期化
        GameScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //スコアの代入
        GameScore.text = score.ToString();

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
            x = Random.Range(-9.0f, 9.0f);
            y = Random.Range(1.0f, 2.0f);
            z = Random.Range(-9.0f, 9.0f);

            number = Random.Range(0, randomEnemy.Length);
            Instantiate(randomEnemy[number], new Vector3(x, y, z), transform.rotation);

            time = 0.0f;
        }

    }

    [System.Serializable]
    public class SaveData
    {
        public int Hscore_S = 0;
    }


    public void Save()
    {
        string json = JsonUtility.ToJson(save);

        StreamWriter streamWriter = new StreamWriter(filePath);
        streamWriter.Write(json);
        streamWriter.Flush();
        streamWriter.Close();
    }

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
