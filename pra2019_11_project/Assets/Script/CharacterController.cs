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
    [SerializeField] protected float speed = 3;

    //0設定なし1HP2攻撃3防御4回復5ニマス進む
    protected int meiroObjectNum = 5;
    protected int[] comandMask=new int[6];
    protected int[] map;
    protected int nowIndex;
    protected int finalNextIndex;
    protected int beforMeiroNum = 3;
    protected int[] moveIndex = new int[4];
    protected Vector3 nowPosition;
    protected Quaternion nowRotation;
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
        int rotationNum = -1;
        float setS = speed * Time.deltaTime;
        if (finalNextIndex - nowIndex == 1)
        {
            rotationNum = 2;
            Quaternion nextQuaternion = Quaternion.Euler(0, 0, -90f) * nowRotation;
            Vector3 nextV3 = new Vector3(2f, 0f, 0f) + nowPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation, nextQuaternion, setS);
            transform.position = Vector3.Lerp(transform.position, nextV3, setS);
            
            /*
            pV3.x = Mathf.Round(pV3.x);
            pV3.y = Mathf.Round(pV3.y);
            pV3.z = Mathf.Round(pV3.z);
            transform.position = pV3;
            */
            Debug.Log(Mathf.Floor(Quaternion.Dot(transform.rotation, nextQuaternion)));
            Debug.Log(Mathf.Abs(Vector3.Distance(transform.position, nextV3)));
            if (Mathf.Floor(Quaternion.Dot(transform.rotation, nextQuaternion)) == 0 && (Mathf.Abs(Vector3.Distance(transform.position, nextV3))<0.01f))
            {
                Quaternion qV3 = transform.rotation;
                Vector3 rV3 = qV3.eulerAngles;
                rV3.x = Mathf.Round(rV3.x);
                rV3.y = Mathf.Round(rV3.y);
                rV3.z = Mathf.Round(rV3.z);
                Vector3 pV3 = transform.position;
                pV3.x = Mathf.Round(pV3.x);
                pV3.y = Mathf.Round(pV3.y);
                pV3.z = Mathf.Round(pV3.z);
                transform.position = pV3;
                transform.rotation = Quaternion.Euler(rV3);
                if (map[finalNextIndex] == 2)
                {
                    GameManager.instance.GetMeiro().GeneratPublic();
                    GameManager.instance.SetPlayerKoudou(4);
                }
                map[nowIndex] = beforMeiroNum;
                beforMeiroNum = map[finalNextIndex];
                map[finalNextIndex] = meiroObjectNum;
                GameManager.instance.GetPlayer().GetComponent<MeiroObjectID>().SetID(finalNextIndex);
                GameManager.instance.SetPlayerKoudou(3);
            }
        }
        else if (finalNextIndex - nowIndex == -1)
        {
            rotationNum = 3;
            Quaternion nextQuaternion = Quaternion.Euler(0, 0, 90f) * nowRotation;
            Vector3 nextV3 = new Vector3(-2f, 0f, 0f) + nowPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation, nextQuaternion, setS);
            transform.position = Vector3.Lerp(transform.position, nextV3, setS);

           
            /*
            transform.position = pV3;
            pV3.x = Mathf.Round(pV3.x);
            pV3.y = Mathf.Round(pV3.y);
            pV3.z = Mathf.Round(pV3.z);
            */
            Debug.Log(Mathf.Floor(Quaternion.Dot(transform.rotation, nextQuaternion)));
            Debug.Log( Mathf.Abs(Vector3.Distance(transform.position, nextV3)));
            if (Mathf.Floor(Quaternion.Dot(transform.rotation, nextQuaternion)) == 0 && (Mathf.Abs(Vector3.Distance(transform.position, nextV3)) <0.01f))
            {
                Quaternion qV3 = transform.rotation;
                Vector3 rV3 = qV3.eulerAngles;
                rV3.x = Mathf.Round(rV3.x);
                rV3.y = Mathf.Round(rV3.y);
                rV3.z = Mathf.Round(rV3.z);
                Vector3 pV3 = transform.position;
                pV3.x = Mathf.Round(pV3.x);
                pV3.y = Mathf.Round(pV3.y);
                pV3.z = Mathf.Round(pV3.z);
                transform.position = pV3;
                transform.rotation = Quaternion.Euler(rV3);
                if (map[finalNextIndex] == 2)
                {
                    GameManager.instance.GetMeiro().GeneratPublic();
                    GameManager.instance.SetPlayerKoudou(4);
                }
                map[nowIndex] = beforMeiroNum;
                beforMeiroNum = map[finalNextIndex];
                map[finalNextIndex] = meiroObjectNum;
                GameManager.instance.GetPlayer().GetComponent<MeiroObjectID>().SetID(finalNextIndex);
                GameManager.instance.SetPlayerKoudou(3);
            }
        }
        else if(finalNextIndex-nowIndex== GameManager.instance.GetMeiroNeighborhood())
        {
            rotationNum = 1;
            Quaternion nextQuaternion = Quaternion.Euler(90f, 0, 0f)*nowRotation;
            Vector3 nextV3 = new Vector3(0f, 0f, 2f) + nowPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation, nextQuaternion, setS);
            transform.position = Vector3.Lerp(transform.position, nextV3, setS);

            
            /*
            pV3.x = Mathf.Round(pV3.x);
            pV3.y = Mathf.Round(pV3.y);
            pV3.z = Mathf.Round(pV3.z);
            transform.position = pV3;
        */
            Debug.Log(Mathf.Floor(Quaternion.Dot(transform.rotation, nextQuaternion)));
            Debug.Log(Mathf.Abs(Vector3.Distance(transform.position, nextV3)));
            if (Mathf.Floor(Quaternion.Dot(transform.rotation, nextQuaternion)) == 0 && (Mathf.Abs(Vector3.Distance(transform.position, nextV3)) <0.01f))
            {
                Quaternion qV3 = transform.rotation;
                Vector3 rV3 = qV3.eulerAngles;
                rV3.x = Mathf.Round(rV3.x);
                rV3.y = Mathf.Round(rV3.y);
                rV3.z = Mathf.Round(rV3.z);
                Vector3 pV3 = transform.position;
                pV3.x = Mathf.Round(pV3.x);
                pV3.y = Mathf.Round(pV3.y);
                pV3.z = Mathf.Round(pV3.z);
                transform.position = pV3;
                transform.rotation = Quaternion.Euler(rV3);
                if (map[finalNextIndex] == 2)
                {
                    GameManager.instance.GetMeiro().GeneratPublic();
                    GameManager.instance.SetPlayerKoudou(4);
                }
                map[nowIndex] = beforMeiroNum;
                beforMeiroNum = map[finalNextIndex];
                map[finalNextIndex] = meiroObjectNum;
                GameManager.instance.GetPlayer().GetComponent<MeiroObjectID>().SetID(finalNextIndex);
                GameManager.instance.SetPlayerKoudou(3);
            }

           
        }
        else if(finalNextIndex - nowIndex == -GameManager.instance.GetMeiroNeighborhood())
        {
            rotationNum = 0;
            Quaternion nextQuaternion = Quaternion.Euler(-90f, 0,0 ) * nowRotation;
            Vector3 nextV3 = new Vector3(0f, 0f, -2f) + nowPosition;
            transform.rotation = Quaternion.Slerp(transform.rotation, nextQuaternion, setS);
            transform.position = Vector3.Lerp(transform.position, nextV3, setS);

            
            
            /*
            
            */
            Debug.Log(Mathf.Floor(Quaternion.Dot(transform.rotation, nextQuaternion)));
            Debug.Log(Mathf.Abs(Vector3.Distance(transform.position, nextV3)));
            if (Mathf.Floor(Quaternion.Dot(transform.rotation, nextQuaternion)) == 0 && (Mathf.Abs(Vector3.Distance(transform.position, nextV3))<0.01f))
            {
                Quaternion qV3 = transform.rotation;
                Vector3 rV3 = qV3.eulerAngles;
                rV3.x = Mathf.Round(rV3.x);
                rV3.y = Mathf.Round(rV3.y);
                rV3.z = Mathf.Round(rV3.z);
                Vector3 pV3 = transform.position;
                pV3.x = Mathf.Round(pV3.x);
                pV3.y = Mathf.Round(pV3.y);
                pV3.z = Mathf.Round(pV3.z);
                transform.position = pV3;
                transform.rotation = Quaternion.Euler(rV3);
                if (map[finalNextIndex] == 2)
                {
                    GameManager.instance.GetMeiro().GeneratPublic();
                    GameManager.instance.SetPlayerKoudou(4);
                }
                map[nowIndex] = beforMeiroNum;
                beforMeiroNum = map[finalNextIndex];
                map[finalNextIndex] = meiroObjectNum;
                GameManager.instance.GetPlayer().GetComponent<MeiroObjectID>().SetID(finalNextIndex);
                GameManager.instance.SetPlayerKoudou(3);
            }

            

           
        }
        


        
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
    public void SetFinalIndex(int a)
    {
        finalNextIndex = a;
    }

    public void NowPosition(Vector3 v3)
    {
        nowPosition = v3;
    }

    public void NowRotation(Quaternion a)
    {
        nowRotation = a;
    }

}
