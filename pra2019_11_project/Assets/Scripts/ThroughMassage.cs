using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroughMassage : MonoBehaviour
{
    public Image back;
    public Text text;
    public Image backButton;
    public Text textButton;

    [HideInInspector]
    public bool onDisplay = false;
    [HideInInspector]
    public bool isChanging = false;
    public Object accese;
    public ItemObject pushBuntton;

    private bool forced = false;

    private void Update()
    {
        //Debug.Log("onDisplay: " + onDisplay);

        if (forced)
        {

        }
    }

    private IEnumerator Message(string mess)
    {
        
        back.color = new Color(back.color.r, back.color.g, back.color.b, 0f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);

        isChanging = true;
        for (int i=0; i <= 30; i++)
        {
            yield return null;
            back.color = new Color(back.color.r, back.color.g, back.color.b, (float)i/30 - 0.5f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, (float)i/30);
        }
        isChanging = false;
        onDisplay = true;
        
    }

    private IEnumerator NoMessage()
    {
        
        back.color = new Color(back.color.r, back.color.g, back.color.b, 0.5f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1f);
        isChanging = true;
        for (int i = 0; i <= 30; i++)
        {
            yield return null;
            back.color = new Color(back.color.r, back.color.g, back.color.b, 0.5f - (float)i / 30);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1f - (float)i / 30);
        }
        isChanging = false;
        onDisplay = false;
        
    }

    private IEnumerator TimeMassage(string mass, float time)
    {
        if (!onDisplay)
        { Message(mass); }
        else
        {  Set_Mess(mass); }
        while (!onDisplay) { yield return null; }

        yield return new WaitForSeconds(time);
        StartCoroutine(NoMessage());
    }

    /// <summary>
    /// メッセージを非表示にする
    /// </summary>
    public void Clear_Message()
    {
        if (onDisplay && !isChanging)
        {
            NoDisplay_Button();
            StartCoroutine(NoMessage());
        }
    }

    /// <summary>
    /// 強制的にメッセージを消去する
    /// </summary>
    public void Clear_Message_F()
    {
        back.color = new Color(back.color.r, back.color.g, back.color.b, 0f);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0f);
        onDisplay = false;

    }

    /// <summary>
    /// メッセージを呼び出す
    /// </summary>
    /// <param name="mess"></param>
    public void Call_Message(string mess, string mess_b, bool displayButton)
    {
        if (!onDisplay && !isChanging)
        {
            if (displayButton) Display_Button(mess_b);
            Set_Mess(mess);
            StartCoroutine(Message(mess_b));
        }
    }

    /// <summary>
    /// メッセージを呼び出す
    /// </summary>
    /// <param name="mess"></param>
    /// <param name="mess_b"></param>
    public void Call_Message(string mess, string mess_b)
    {
        Call_Message(mess, mess_b, true);
    }

    /// <summary>
    /// タイムメッセージを呼び出す
    /// </summary>
    /// <param name="text"></param>
    /// <param name="second"></param>
    public void Call_MessageTime(string text, float second)
    {
        //if (onDisplay) Clear_Message_F();

        NoDisplay_Button();
        StartCoroutine(TimeMassage(text, second));
    }

    /// <summary>
    /// メッセージを設定する
    /// </summary>
    /// <param name="mess"></param>
    public void Set_Mess(string mess)
    {
        text.text = mess;
    }

    /// <summary>
    /// ボタンを表示する
    /// </summary>
    /// <param name="mess"></param>
    public void Display_Button(string mess)
    {
        textButton.text = mess;
        textButton.color= new Color(textButton.color.r, textButton.color.g, textButton.color.b, 1);
        backButton.color = new Color(backButton.color.r, backButton.color.g, backButton.color.b, 1);

        //button.SetActive(true);
    }

    /// <summary>
    /// ボタンを表示しない
    /// </summary>
    public void NoDisplay_Button()
    {
        textButton.color = new Color(textButton.color.r, textButton.color.g, textButton.color.b, 0);
        backButton.color = new Color(backButton.color.r, backButton.color.g, backButton.color.b, 0);

        //button.SetActive(false);
    }


}
