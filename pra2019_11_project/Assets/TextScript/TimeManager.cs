using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public GameObject time_object = null;
    public float nowtime_ui = 10.0f;
    public float timecount = 1.0f;
    public bool fin_time;

    // Start is called before the first frame update
    void Start()
    {
        timecount = 1.0f;
        fin_time = false;
    }

    void Update()
    {
        timecount -= Time.deltaTime;
        if (timecount <= 0)
        {
            //残り時間の表示
            Text time_text = time_object.GetComponent<Text>();
            time_text.text = "Time:" + nowtime_ui;
            nowtime_ui -= Time.deltaTime;
            //残り時間が0になったら0.00を表示
            if (nowtime_ui <= 0)
            {
                time_text.text = "Time:0.00";
                fin_time = true;
            }
        }
    }
}
