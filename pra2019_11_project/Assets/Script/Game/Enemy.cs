using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    [SerializeField]public float speed = 1f;
    [SerializeField]public Vector3 playerPos;
    [SerializeField] public Vector3 enemyPos;
    public GameObject player;
    public Transform playerTransform;
    public Transform enemyTransform;
    public float length;
    Vector3 diff;

    void Start()
    {
        enemyTransform = this.gameObject.GetComponent<Transform>();
        playerTransform = player.gameObject.GetComponent<Transform>();
    }

    void Update()
    {
        enemyPos = enemyTransform.position;
        playerPos = playerTransform.position;
        diff = playerPos - enemyPos;
        if (Vector3.Distance(playerPos, enemyPos) <= length)             //Vector3.Distance(a,b)でa,b間の距離
        {
            transform.LookAt(diff);
            enemyTransform.position = diff;
        }
    }

    //衝突時の判定
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("当たった");
            SceneManager.LoadScene("GameOverScene");
        }
        else if (collision.gameObject.name != "Player" && enemyTransform.position.y<=0.68f)
        {
            Debug.Log(collision.gameObject.name);
            Quaternion enemyRot = Quaternion.Inverse(enemyTransform.rotation);
            enemyTransform.rotation = enemyRot;
        }
    }
}

