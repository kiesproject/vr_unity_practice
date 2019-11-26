using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    /// <summary>
    /// ☆課題の主人公を追いかけるscript
    /// 子オブジェクトのsearchareaにプレイヤーが入ったら追いかける
    /// 
    /// </summary>
    Rigidbody rb;
    public float speed=0.02f;
    //計算用
    private float MoveX, MoveZ;
    //何を追いかけるか
    public Transform Target;
    //見つけているならTrue
    public bool find;

    private void Start()
    {
        //prefab化しているのでここでPlayerを取得しておく
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        //見つけているなら
        if (find==true)
        {
            //☆追いかけるスクリプトの本体
            //(from,to,t)fromからtoへt時間で向く
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Target.position - transform.position), 0.3f);

            transform.position += transform.forward * speed;
        }

        //もしy<-2 つまりフィールドから落ちたらdestroy
        //thisはこのインスタンス自身を示す
        if (this.gameObject.transform.position.y < -2)
        {
            Destroy(this.gameObject);
        }
    }
        

    //GameControlleのコルーチンに入れてしまったのであまりvoidにした意味がなかった
    public void Chase()
    {
        find = true;
    }

    public void Stay()
    {
        find = false;
    }
}
