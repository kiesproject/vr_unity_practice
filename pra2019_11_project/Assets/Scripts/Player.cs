using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;      //速さ
    public GameObject text; //ゲームオーバーのオブジェクト
    public GameObject text2; //ゲームクリアのオブジェクト
    public GameObject text3; //タイマーの表示
    public bool atack = false;//判定

    void Start()
    {
        this.transform.position = new Vector3(0, -0.4f, 0);  //初期ポジションの固定
    }
    //x = -19～19
    //z = -10.25～10.5

    void Update()
    {
        Move();        //Move()を使う

        if(speed <= 8.0)  //もしスピードが8.0以下ならばTimer2が10になるごとにspeedを0.2増やす
        {
            if (controler.instance.timer2 >= 9.955)
            {
                speed += 0.2f;
            }
        }
        if(text3 == null)  //text3が無かったらatackをfalseにする
        {
            atack = false;
        }
    }

    private void Move()
    {　
        //プレイヤーがフィールドの範囲内にいるとき対応したボタンで対応した動きをする

        if (transform.position.x < 18.5 && transform.position.x > -18.5)　
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(0.05f * speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-0.05f * speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, 0, 0.05f * speed);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, 0, -0.05f * speed);
            }
        }
        else if(transform.position.x >= 18.5)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Translate(-0.05f * speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, 0, 0.05f * speed);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, 0, -0.05f * speed);
            }
        }
        else if(transform.position.x <= -18.5)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Translate(0.05f * speed, 0, 0);
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, 0, 0.05f * speed);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, 0, -0.05f * speed);
            }
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(atack == false)//アタックがfalseの時
        {
            if (other.tag == "Enemy")  //もしエネミーに接触した場合ゲームオーバーのオブジェクトをアクティブにし、このゲームオブジェクトを消去する
            {
                text.SetActive(true);
                this.gameObject.SetActive(false);
            }
        }
        else //atackがtrueだったら無敵
        {

        }
        if(other.tag == "Clear") //もしクリアした場合ゲームクリアのオブジェクトをアクティブにし、このゲームオブジェクトを消去する
        {
            text2.SetActive(true);
            this.gameObject.SetActive(false);
        }

        if(other.tag == "Item") //もしアイテムに触ったらアタックをtrueにする
        {
            atack = true;
            text3.SetActive(true);
        }
    }
}
