using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    /// <summary>
    /// playerのジャンプや移動に関するスクリプト
    /// destinationに触れたらdestinationを破壊しtimeadd関数をgamecontrollerから呼び出す
    /// </summary>
    Rigidbody rb;
    public float speed=3f;
    public GameContoroller gamecontroller;
    private float moveX ,moveZ;
    //ジャンプは一ゲーム中3回まで
    public int jumpcount=3;
    [SerializeField] private bool jumping = false;
    
    private void Start()
    {
        //試しにスタートで取得してみる
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //ここで十字キーによる移動量を定義
        moveX = Input.GetAxis("Horizontal") * speed;
        moveZ = Input.GetAxis("Vertical") * speed;
        Vector3 direction = new Vector3(moveX, 0, moveZ);
    }

    private void FixedUpdate()
    {
        //Updateでの情報を代入して移動を実行
        rb.velocity = new Vector3(moveX, 0, moveZ);

        //スペースキーでジャンプメソッドを実行
        if (Input.GetKeyDown("space")&&jumping==false)
        {
            jump();
        }

        //ジャンプ中に下方向に力を加え続ける、
        if (jumping)
        {
            //ForceModeはAddForceのオプション的なもの、質量を無視して継続的な力を与える
            rb.AddForce(Vector3.down * 70,ForceMode.Acceleration);
        }
    }

    //jumpメソッド
    void jump()
    {
        if (jumpcount != 0)
        {
            rb.AddForce(Vector3.up * 2300);
            jumpcount -= 1;
        }
    }

    //Fieldに接しているならjumpingをfalseに
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == ("Field"))
        {
            jumping = false;
        }

    }

    //Fieldから出た場合　空中に浮いているのでjumping=trueに
    void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.tag == ("Field"))
        {
            jumping = true;
        }

    }
    //Destinationに到着した場合 destinationを破壊してtimeaddメソッドを実行
    private void OnTriggerEnter(Collider Collider)
    {
        if (Collider.gameObject.tag == ("Destination"))
        {
            Destroy(Collider.gameObject);
            gamecontroller.timeadd();
        }
    }




}
