using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jamp : MonoBehaviour
{
    public GameObject Player;
    private Rigidbody PlayerRigid;//PlayerオブジェクトのRigidbobyを保管する
    public float Upspeed;　　　　//ジャンプのスピード
    // Use this for initialization
    void Start()
    {
        PlayerRigid = Player.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "Ground" && Input.GetKey(KeyCode.Space))
        {
            PlayerRigid.AddForce(transform.up * Upspeed);
        }
    }
}