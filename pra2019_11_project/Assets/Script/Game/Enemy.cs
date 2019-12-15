using UnityEngine;
using UnityEngine.SceneManagement;
public class Enemy : MonoBehaviour
{
    [SerializeField] public float speed = 2.5f;
    [SerializeField] public Vector3 playerPos;
    [SerializeField] public Vector3 enemyPos;
    public GameObject player;
    public Transform playerTransform;
    public Transform enemyTransform;
    public float length = 10;
    private bool playerCol;
    [SerializeField] public Rigidbody rb;

    void Start()
    {
        //*** ======================================================================================================================================
        //*** [改善]enemyTransformとplayerTransformはTransform型で宣言されているのでここで改めてGetComponent<Transform>()する必要はありません。
        //*** ======================================================================================================================================

        enemyTransform = this.gameObject.GetComponent<Transform>();
        playerTransform = player.gameObject.GetComponent<Transform>();
        playerCol = false;

        //*** ===================================================================================================
        //*** [アドバイス]this.gameObjectにRigidbodyがアタッチされているので、rbをprivateにした方がいいでしょう。
        //***             カプセル化の観点からpublicにする必要の無いものはprivateにする方がいいでしょう。
        //*** ===================================================================================================

        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        enemyPos = enemyTransform.position;                             //Enemyの座標取得
        playerPos = playerTransform.position;                           //Playerの座標取得

        //*** ============================================================================
        //*** [改善]playerColはbool型なので(playerCol == true)はplayerColのみで書けます。
        //*** ============================================================================

        if (playerCol == true)
        {
            SceneManager.LoadScene("GameOverScene");
        }        

        if (Vector3.Distance(playerPos, enemyPos) <= length)             //Vector3.Distance(a,b)でa,b間の距離
        {
            this.transform.LookAt(playerPos);
            rb.velocity = transform.forward * speed;
        }
        else
        {
            Vector3 randomPos  = new Vector3(UnityEngine.Random.Range(-30.0f,30.0f),0, UnityEngine.Random.Range(-30.0f, 30.0f));
            this.transform.LookAt(randomPos);
            rb.velocity = transform.forward * speed;
        }
        
    }

    //衝突時の判定
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerCol = true;
        }
        if (collision.gameObject.name == "Wall")
        {
            Quaternion enemyRot = Quaternion.Inverse(enemyTransform.rotation);
            enemyTransform.rotation = enemyRot;
            rb.velocity = transform.forward * speed;
        }
    }
}

