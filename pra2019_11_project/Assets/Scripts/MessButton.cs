using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MessButton : MonoBehaviour
{
    private Button button;

    public void OnClick()
    {
        //Debug.Log("押された!");  // ログを出力
        GameManager.instance.GetItem();
    }

    private void Start()
    {
        button = GetComponent<Button>();
        var trigger = button.GetComponent<EventTrigger>();

        // 登録するイベントを設定する
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        // リスナーは単純にLogを出力するだけの処理にする
        entry.callback.AddListener((data) => { Debug.Log("PointerEnter"); GameManager.instance.cursorOnUI = true; });

        var entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { Debug.Log("PointerExit"); GameManager.instance.cursorOnUI = false; });

        // イベントを登録する
        trigger.triggers.Add(entry);
        trigger.triggers.Add(entry2);

    }

}
