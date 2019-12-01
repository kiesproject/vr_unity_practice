using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Button : MonoBehaviour
{
    public ButtonType buttonType;

    public enum ButtonType
    {
        UP, DOWN, USE, DEL
    }

    public void OnClick()
    {
        if (GameManager.instance != null)
        {

            switch (buttonType)
            {
                case ButtonType.UP:
                    {
                        GameManager.instance.Scroll_Inventory(false);
                    }
                    break;
                case ButtonType.DOWN:
                    {
                        GameManager.instance.Scroll_Inventory(true);
                    }
                    break;
                case ButtonType.USE:
                    {
                        GameManager.instance.UseItem();
                    }
                    break;
                case ButtonType.DEL:
                    {
                        var name = GameManager.instance.itemList[GameManager.instance.cursorInventory].Name;
                        GameManager.instance.ItemDelete();
                        GameManager.instance.throughMassage.Call_MessageTime(string.Format("{0}を捨てました。", name), 1f);

                    }
                    break;

            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
