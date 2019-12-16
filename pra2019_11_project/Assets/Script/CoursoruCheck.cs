using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoursoruCheck : MonoBehaviour
{
    //MainCameraに付ける
    public string tagname = "Plane";
    public string tagname1 = "Finish";
    public string tagname2 = "Enemy";
    public string tagname3 = "Player";
    private GameObject Player;
    private Ray ray;
    private RaycastHit hit_info;
    private float max_distance;
    private bool setNeigborTrue = false;
    // Start is called before the first frame update
    void Start()
    {
        Player= GameManager.instance.GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hit_info = new RaycastHit();
        max_distance = 100f;

        bool is_hit = Physics.Raycast(ray, out hit_info, max_distance);

        if (is_hit)
        {
            setNeigborTrue = false;
            PlayerNeibordObjectChack();
            if ((hit_info.transform.tag == tagname || hit_info.transform.tag == tagname1)&&setNeigborTrue)
            {
                hit_info.transform.GetComponent<PlaneCoursoru>().SetPointCoursoru();
                if (Input.GetMouseButtonUp(0))
                {
                    GameManager.instance.GetPlayer().GetComponent<PlayerController>().SetFinalIndex(hit_info.transform.GetComponent<MeiroObjectID>().GetID());
                    GameManager.instance.SetPlayerKoudou(1);
                    GameManager.instance.GetPlayer().GetComponent<PlayerController>().NowPosition(GameManager.instance.GetPlayer().transform.position);
                    GameManager.instance.GetPlayer().GetComponent<PlayerController>().NowRotation(GameManager.instance.GetPlayer().transform.rotation);
                }

            }
            else if (hit_info.transform.tag == tagname2&&setNeigborTrue)
            {
                hit_info.transform.GetComponent<PlaneCoursoru>().SetAttackCoursoru();
                if (Input.GetMouseButtonUp(0))
                {
                    GameManager.instance.SetPlayerKoudou(2);
                }
            }
        }
    }


    private void PlayerNeibordObjectChack()
    {
        int[] vs = Player.GetComponent<PlayerController>().MapIndex();
        for(int i = 0; i < vs.Length; i++)
        {
            //Debug.Log("GetIDは" + hit_info.transform.GetComponent<MeiroObjectID>().GetID());
            if (vs[i] > -1)
            {
                //*** ===============================================================================
                //*** [改善] hit_infoに中身が入っているのか確認してないんじゃないですかぁ？？？？？
                //*** ===============================================================================
                if (hit_info.transform.GetComponent<MeiroObjectID>()) //*** 講評するため追加しました。
                {
                    if (vs[i] == hit_info.transform.GetComponent<MeiroObjectID>().GetID())
                    {
                        setNeigborTrue = true;
                    }
                }
            }
        }
    }

}
