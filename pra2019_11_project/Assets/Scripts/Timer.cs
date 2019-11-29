using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{    
    public float time = 180;//制限時間
    Text timerText;

    // Start is called before the first frame update
    void Start()
    {
        timerText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        //マイナスは表示しない
        if (time < 0)//時間内にゴールしなければ、ゲームオーバーシーンへ遷移
        {
            SceneManager.LoadScene("GameOver");
        }
        timerText.text = "time" + ((int)time).ToString();
    }
}
