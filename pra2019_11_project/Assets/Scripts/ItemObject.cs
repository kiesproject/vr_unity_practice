using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item item;
    protected bool isMess = false;
    protected bool isMess_dump = false;
    [SerializeField]
    private GameObject rootObject;

    [SerializeField]
    private Rigidbody box;

    protected ThroughMassage tm;


    private void Start()
    {
        //item = new Gold();
        
    }

    private void Update()
    {
        

        if (GameManager.instance.throughMassage != null)
        {
            if (tm == null) tm = GameManager.instance.throughMassage;

            if (isMess)
            {
                if (tm.accese == null && !tm.onDisplay)
                {
                    GameManager.instance.player.culletTarget = this;
                    tm.Call_Message("宝箱が置いてある…", "開ける");
                    tm.accese = this;

                }
            }
            else
            {
                if (tm.accese == this && tm.onDisplay)
                {
                    GameManager.instance.player.culletTarget = null;
                    tm.Clear_Message();
                    tm.accese = null;
                }
            }
            
        }

        
        //Debug.Log(string.Format("[ItemObject] 「{0}」 isMess : {1}", gameObject.name, isMess));
    }

    private void OnDestroy()
    {
        if (GameManager.instance.labyrinth.listBlock.Contains(gameObject))
        {
            GameManager.instance.labyrinth.listBlock.Remove(gameObject);
        }

        if (tm.accese == this && tm.onDisplay)
        {
            GameManager.instance.player.culletTarget = null;
            tm.Clear_Message();
            tm.accese = null;
        }
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
        Get_Item(true);
    }

    public virtual void Get_Item(bool destroy)
    {
        if (item != null)
        {
            if (GameManager.instance.throughMassage != null)  
            {

                if (GameManager.instance.itemList.Count < GameManager.instance.itemCapa)
                {
                    GameManager.instance.throughMassage.Call_MessageTime(string.Format("「{0}」を手に入れた。", item.Name), 2f);

                    item.Get_Item();
                    if (destroy)
                    {
                        Destroy(rootObject);
                        if (GameManager.instance.throughMassage.accese == this) GameManager.instance.throughMassage.accese = null;
                    }
                }
                else
                {
                    GameManager.instance.throughMassage.Call_MessageTime("もうアイテムを持てない", 2f);
                    GameManager.instance.throughMassage.accese = null;
                }
            }
        }
    }

    public void Create_Item<T>()
        where T : Item, new()
    {
        var item = new T();
        //Debug.Log(item);
        var gm = GameManager.instance;
        item.Create(gm.luck, gm.unluck);
        //Debug.Log(item.Name);

        this.item = item;
    }

    

}
