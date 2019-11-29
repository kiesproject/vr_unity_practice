using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool tokotoko;
    public AudioClip walk;
    public float speed;
    public int angle;
    private CharacterController characterController;
    private Animator animator;
    AudioSource audioSource;

    void Start()
    {
        tokotoko = false;
        speed += 1;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow))//左回転
        {
            transform.Rotate(0, -1 * angle * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))//右回転
        {
            transform.Rotate(0, angle * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))//前進＆アニメーション（歩く）
        {
            characterController.Move(this.gameObject.transform.forward * Time.deltaTime * speed);
            animator.SetBool("IsWalk", true);
            tokotoko = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))//歩くモーションを止める。音も止める。
        {
            animator.SetBool("IsWalk", false);
            tokotoko = false;
            audioSource.Stop();
        }
        /*
        if (Input.GetKey(KeyCode.DownArrow))//方向キーの↓を使おうと頑張ってみただけ
        {
            characterController.Move(this.gameObject.transform.forward * Time.deltaTime * speed );
        }
        */
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Finish")//クリアシーンへ遷移
        {
            SceneManager.LoadScene("Clear");
            // Debug.Log("OK");
        }
    }
}
