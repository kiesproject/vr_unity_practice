using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySystem : MonoBehaviour
{
    public Text nameText;
    public Text descText;
    public GameObject header;
    public GameObject upButton;
    public GameObject downButton;
    public GameObject useButton;
    public GameObject delButton;
    public GameObject Equipment;

    //--- --- --- --- --- --- --- ---
    private GameManager gm;
    private Text textHeader;
    private Text textUse;
    

    // Start is called before the first frame update
    void Start()
    {
        var trigger = GetComponent<EventTrigger>();

        // 登録するイベントを設定する
        var entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;

        // リスナーは単純にLogを出力するだけの処理にする
        entry.callback.AddListener((data) => { GameManager.instance.cursorOnUI = true; });

        var entry2 = new EventTrigger.Entry();
        entry2.eventID = EventTriggerType.PointerExit;
        entry2.callback.AddListener((data) => { GameManager.instance.cursorOnUI = false; });

        // イベントを登録する
        trigger.triggers.Add(entry);
        trigger.triggers.Add(entry2);


        gm = GameManager.instance;
        textHeader = header.GetComponentInChildren<Text>();
        textUse = useButton.GetComponentInChildren<Text>();
    }



    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance != null)
        {
            Display_Header();
            if (gm.itemList.Count != 0)
            {
                Display_Item(gm.cursorInventory);
                Display_Desc(gm.cursorInventory);
            }

            if (gm.player.weapon == gm.itemList[gm.cursorInventory] || gm.player.armor == gm.itemList[gm.cursorInventory])
            {
                Display_UseButton(false);
                Display_DelButton(false);
                Equipment.SetActive(true);
            } else {
                Display_UseButton(true);
                Display_DelButton(true);
                Equipment.SetActive(false);
            }
        }
    }


    public void Display_Item(int index)
    {
        if (index < gm.itemList.Count)
        {
            var item = gm.itemList[index];
            nameText.text = item.Name;
        }

    }

    public void Display_Desc(int index)
    {
        if (index < gm.itemList.Count)
        {
            var item = gm.itemList[index];
            descText.text = item.Description;
        }
    }

    public void Display_Header()
    {
        if (GameManager.instance != null) {

            textHeader.text = string.Format("アイテム所持数 {0}/{1}", GameManager.instance.itemList.Count, GameManager.instance.itemCapa);
        }
    }

    public void Display_UseButton(bool offON)
    {
        useButton.SetActive(offON);
    }

    public void Display_DelButton(bool offON)
    {
        delButton.SetActive(offON);
    }
}
