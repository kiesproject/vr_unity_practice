using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    
    public GameObject GameOver_A;
    public Rigidbody rb;
    public float move = 1.0f; //インスペクターで速度の変更可能

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームオーバー時、プレイヤーの操作を受け付けなくする
        if (GameOver_A.activeSelf == true)
        {
            return;
        }

        //WASDキーでの移動
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * move;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = transform.right * -move;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * move;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = transform.forward * -move;
        }
        
    }

    //ゲームオーバー画面の表示、5秒後にタイトルへ
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            GameOver_A.SetActive(true);

            Invoke("ReturnTitle", 5); //5秒後にReturnTitleを呼び出す
        }
    }

    void ReturnTitle()
    {
        //タイトルへ戻し、カーソルを表示する
        SceneManager.LoadScene("Title");
        Cursor.lockState = CursorLockMode.None;
    }
}
