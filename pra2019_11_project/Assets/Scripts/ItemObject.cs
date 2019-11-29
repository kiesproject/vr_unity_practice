using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    public LayerMask mask;
    private bool isMess = false;
    private bool isMess_dump = false;

    private void Start()
    {
        item = new Gold();
    }

    private void Update()
    {
        if (isMess_dump != isMess)
        {
            if (isMess)
            {
                GameManager.instance.player.culletTarget = this;
                GameManager.instance.throughMassage.Call_Message("宝箱が置いてある…", "開ける");

            }
            else
            {
                GameManager.instance.player.culletTarget = null;
                GameManager.instance.throughMassage.Clear_Message();

            }
        }

        isMess_dump = isMess;
    }

    private void OnTriggerStay(Collider other)
    {
        if (GameManager.instance.player != null)
        {
            //if (GameManager.CompareLayer(mask, other.gameObject.layer))
            {

                if (other.gameObject.tag == "Player")
                {
                    isMess = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GameManager.instance.player != null)   
        {
            if (other.gameObject.tag == "Player")
            {
                isMess = false;
            }
        }

    }

    public void Get_Item()
    {
        if (GameManager.instance.throughMassage != null)
        {
            GameManager.instance.throughMassage.Call_MessageTime(string.Format("「{0}」を手に入れた。", item.NAME), 4f);
            item.Get_Item();
            Destroy(gameObject);
        }
    }

    public Item Create_Item<T>()
        where T : Item, new()
    {
        var item = new T();
        var gm = GameManager.instance;
        item.Create(gm.luck, gm.unluck);

        return item;
    }

}
