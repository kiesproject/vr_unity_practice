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
    [SerializeField]
    public int state_res = 1;
    [SerializeField]
    public int state_speed = 1;
    [SerializeField]
    public int state_knoc = 1;

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
        if (GameManager.instance.state == GameManager.State.GAME)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.state == GameManager.State.GAME)
        {
            if (myCamera != null)
            {
                Vector3 dirMove = myCamera.Get_MoveForce(vertical, horizontal);
                float speedL = speed + state_speed;
                if (armor != null) speedL += armor.add_speed_p * state_speed; 


                rig.AddForce(coefficient * (speedL * dirMove - rig.velocity));
            }
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
            int resL = state_res;
            if (armor != null) resL += Mathf.RoundToInt(armor.add_resistance_p * state_res);

            hp -= damage - resL;
            StartCoroutine(NoDamage());
        }
    }

    public void KnockBack(Transform transform, float powor)
    {
        Vector3 v = this.transform.position - transform.position;
        Vector3 vn = new Vector3(v.x, 0, v.z).normalized;

        rig.velocity += vn * powor * ((armor != null) ? armor.add_res_knok_p : 1);
    }

    private void Down()
    {
        GameManager.instance.state = GameManager.State.GAMROVER;
        GameManager.instance.GameOver.SetActive(true);
    }

    public void Drinking(Drink drink)
    {
        hp += drink.heal;
        if (hp > max_hp) hp = max_hp;
        state_res += drink.addResistance;
        state_speed += drink.addSpeed;

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
}
