using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThroughMassage : MonoBehaviour
{
    public Image back;
    public Text text;
    public GameObject button;
    private bool onDisplay = false;
    private bool isChanging = false;

    public ItemObject pushBuntton;

    private void Update()
    {
        
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
        NoMessage();
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
    /// メッセージを呼び出す
    /// </summary>
    /// <param name="mess"></param>
    public void Call_Message(string mess, string mess_b)
    {
        if (!onDisplay && !isChanging)
        {
            Display_Button(mess);
            Set_Mess(mess);
            StartCoroutine(Message(mess_b));
            
        }
    }

    public void Call_MessageTime(string text, float second)
    {
        NoDisplay_Button();
        StartCoroutine(TimeMassage(text, second));
    }

    public void Set_Mess(string mess)
    {
        text.text = mess;
    }

    public void Display_Button(string mess)
    {
        button.transform.GetChild(0).GetComponent<Text>().text = mess;
        button.SetActive(true);
    }

    public void NoDisplay_Button()
    {
        button.SetActive(false);
    }


}
