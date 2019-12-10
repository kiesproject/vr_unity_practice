using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public Transform MY;
    public Transform MX;

    public GameObject GameOver_A;
    public GameObject Sphere;

    private float reloadTime = 0.4f; //発射間隔
    private float time = 0.0f;

    // Use this for initialization
    void Start()
    {
        MY = transform.parent;
        MX = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //マウスによるエイム操作
        float X_Rotation = Input.GetAxis("Mouse X");
        float Y_Rotation = Input.GetAxis("Mouse Y");
        MY.transform.Rotate(0, X_Rotation * 3, 0);
        MX.transform.Rotate(-Y_Rotation * 3, 0, 0);

        //ゲームオーバー時、プレイヤーの操作を受け付けなくする
        if (GameOver_A.activeSelf == true)
        {
            return;
        }

        //Rayの作成
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        time += Time.deltaTime;

        //Rayが当たっているときのみ発射可能
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            //射撃、発射間隔
            if (Input.GetMouseButton(0) && time > reloadTime)
            {
                var bullet = Instantiate(Sphere, transform.position, Quaternion.identity);

                var ep = bullet.GetComponent<Bullet>();
                ep.enemy_Poss = hit.collider.gameObject;

                time = 0.0f;
            }
        }
    }
}
