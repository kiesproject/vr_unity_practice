using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text GameScore;

    [HideInInspector]
    public int score = 0;

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

        //スコアの初期化
        GameScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //スコアの代入
        GameScore.text = score.ToString();

        time += Time.deltaTime;

        //敵のランダム出現
        if(time > appearTime)
        {
            x = Random.Range(-9.0f, 9.0f);
            y = Random.Range(1.0f, 2.0f);
            z = Random.Range(-9.0f, 9.0f);

            number = Random.Range(0, randomEnemy.Length);
            Instantiate(randomEnemy[number], new Vector3(x, y, z), transform.rotation);

            time = 0.0f;
        }

    }
}
