using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    public GameObject start_object = null;
    public float timecount_start;

    // Start is called before the first frame update
    void Start()
    {
        timecount_start = 1.0f;
    }
    // Update is called once per frame
    void Update()
    {
        //ゲーム開始時のUIの表示
        Text start_text = start_object.GetComponent<Text>();
        start_text.text = "GAME START !!";
        timecount_start -= Time.deltaTime;
        if (timecount_start <= 0)
        {
            Destroy(start_object);
        }
    }
}