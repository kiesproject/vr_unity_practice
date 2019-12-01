using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IButtle
{
    int level = 1;
    int hp = 25;
    int max_hp = 25;
    bool isDead = false;
    int damage = 10;

    public float speed = 10;

    private Rigidbody rig;
    [SerializeField]
    private LayerMask layer;

    public int HP() { return hp; }
    public int MAX_HP() { return max_hp; }


    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        if (GameManager.instance.player == null) return;

        //if (Check_RoundPlayer())
        {
            Debug.Log("追う");
            Chase(GameManager.instance.player.transform.position);
        }

        //Debug.Log(string.Format("HP: {0} MAX_HP: {1}", HP(), MAX_HP()));
    }

    private void Chase(Vector3 target)
    {
        if (rig != null)
        {
            var moveV = (target - transform.position);
            var moveV_n = new Vector3(moveV.x, 0, moveV.z).normalized ;
            //Debug.Log(string.Format("moveV: {0} moveV_n: {1}", moveV, moveV_n));
            rig.velocity = speed * moveV_n * (1 + (level - 1) * 0.1f);
        }
    }

    private void Down()
    {
        Destroy(gameObject);
    }

    private void Check_Down()
    {
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            Down();
        }
    }

    public void AddDamage(int damege, GameObject attacker)
    {
        hp -= damege;
        Check_Down();
    }

    /// <summary>
    /// プレイヤーが一直線上にいるかどうか
    /// </summary>
    /// <returns></returns>
    private bool Check_RoundPlayer()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.player != null)
            {
                RaycastHit hit;
                int distance = 10;
                Ray ray = new Ray(transform.position, (GameManager.instance.player.transform.position - transform.position).normalized);
                if (Physics.Raycast(ray, out hit, distance, GameManager.instance.BlockLayer))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

        }

        return false;
    }

    public void KnockBack(Transform transform, float powor)
    {
        //ノックバックは行わない
    }

    private void OnCollisionStay(Collision c)
    {
        if (GameManager.CompareLayer(layer, c.collider.gameObject.layer))
        {
            c.collider.gameObject.GetComponent<IButtle>().AddDamage(damage, gameObject);
            c.collider.gameObject.GetComponent<IButtle>().KnockBack(transform, 10);
        }
    }
}
