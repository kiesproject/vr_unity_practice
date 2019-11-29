using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IButtle
{
    [SerializeField]
    private MyCamera myCamera;

    float horizontal = 0;
    float vertical = 0;
    [SerializeField]
    float coefficient = 200;
    [SerializeField]
    float speed = 2.5f;

    //--- --- --- --- --- ---

    int hp = 100;
    int max_hp = 100;
    [HideInInspector]
    public Weapon weapon;
    [HideInInspector]
    public Armor armor;
    [HideInInspector]
    public ItemObject culletTarget;
    [SerializeField]
    public int bullets = 10;

    bool isNoDamage = false;
    bool isDead = false;

    Rigidbody rig;

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
        //Debug.Log(string.Format("hp: {0} maxHp: {1}", HP(), MAX_HP()));

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (myCamera != null)
        {
            Vector3 dirMove = myCamera.Get_MoveForce(vertical, horizontal);

            rig.AddForce(coefficient * (speed * dirMove - rig.velocity));
        }
    }

    /// <summary>
    /// 無敵時間の有効と無効の切り替え
    /// </summary>
    /// <returns></returns>
    IEnumerator NoDamage()
    {
        isNoDamage = true;
        yield return new WaitForSeconds(3);
        isNoDamage = false;
    }

    /// <summary>
    /// プレイヤーをテレポートさせる
    /// </summary>
    /// <param name="poss"></param>
    public void Telport(Vector3 poss)
    {
        transform.position = poss;
    }

    public void AddDamage(int damage, GameObject attacker)
    {
        if (!isNoDamage)
        {
            hp -= damage;
            StartCoroutine(NoDamage());
        }
    }

    public void KnockBack(Transform transform, float powor)
    {
        Vector3 v = this.transform.position - transform.position;
        Vector3 vn = new Vector3(v.x, 0, v.z).normalized;
        rig.velocity += vn * powor;
    }
}
