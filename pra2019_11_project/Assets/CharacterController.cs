using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected struct stetas
    {
        public int Level;
        public int HP;
        public int Attack;
        public int Diffence;
    }
    stetas Stetas = new stetas();

    public GameObject[] Comand=new GameObject[6];
    protected int comandObjectID;
    //0設定なし1HP2攻撃3防御4回復5ニマス進む
    protected int[] comandMask=new int[6];
    protected int[] map;
    protected int nowIndex;
    protected int[] moveIndex = new int[4];
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void StetasUpdate()
    {
        Stetas.Level = 1;
        Stetas.HP = 100;
        Stetas.Attack = 3;
        Stetas.Diffence = 3;
    }

    protected void GetComand()
    {
        for (int i = 0; i < Comand.Length; i++)
        {
            comandMask[i] = Comand[i].GetComponent<ComandSelectID>().GetComandID();
        }
    }

    protected void GetComandTransform()
    {
        for (int i = 0; i < Comand.Length; i++)
        {
            if (Comand[i].transform.forward == Vector3.forward)
            {
                comandObjectID = i;
                break;
            }
        }
    }

    protected void SelectComand()
    {
       switch(comandMask[comandObjectID])
        {
            case 0:

                break;
            case 1:
                break;
            case 2:
                Attack();
                break;
            case 3:
                Diffence();
                break;
            case 4:
                Cure();
                break;
            case 5:
                SpeedUp();
                break;

        }
    }

    protected void Map()
    {
        int neigboor = GameManager.instance.GetMeiroNeighborhood();
        map = GameManager.instance.GetMap();
 
        if (map[nowIndex - neigboor] != 0)
        {
            moveIndex[0] = nowIndex - neigboor;
        }
        else
        {
            moveIndex[0] = -1;
        }

        if (map[nowIndex + neigboor] != 0)
        {
            moveIndex[1] = nowIndex + neigboor;
        }
        else
        {
            moveIndex[1] = -1;
        }
        if (map[nowIndex + 1] != 0)
        {
            moveIndex[2] = nowIndex + 1;
        }
        else
        {
            moveIndex[2] = -1;
        }
        if (map[nowIndex -1] != 0)
        {
            moveIndex[3] = nowIndex - 1;
        }
        else
        {
            moveIndex[3] = -1;
        }
    }

    protected void Move()
    {
        
    }

    protected void SpeedMove()
    {

    }

    protected void Attack()
    {

    }

    protected void Diffence()
    {

    }

    protected void Cure()
    {


    }

    protected void SpeedUp()
    {

    }

    public int[] MapIndex()
    {
        return moveIndex;
    }

    protected void SetNowIndex()
    {
        nowIndex = GameManager.instance.GetPlayer().GetComponent<MeiroObjectID>().GetID();
    }

}
