using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI_Controller : MonoBehaviour
{
    [SerializeField]
    private Image hpui;
    [SerializeField]
    private Image hpui_m;
    [SerializeField]
    private Text text;
    [SerializeField]
    private GameObject[] keySprite;
    [SerializeField]
    private Text textGold;
    [SerializeField]
    private Text textBullet;
    [SerializeField]
    private Text textWeapon;
    [SerializeField]
    private Text textArmor;

    private Player player;
    private float memo = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            textGold.text = GameManager.instance.golds.ToString() + "G";
            textBullet.text = GameManager.instance.player.bullets.ToString() + "発";

            if (player.weapon != null)
            {
                textWeapon.text = player.weapon.Name;
            }
            else
            {
                textWeapon.text = "装備なし";
            }

            if (player.armor != null)
            {
                textArmor.text = player.armor.Name;
            }
            else
            {
                textArmor.text = "装備なし";
            }

            //---- ---- ---- ----- ----- ---- --- ---- ---- ---- ----- -----

            KeyDisplay(GameManager.instance.Get_KeyState());

            //HP表示変更
            text.text = string.Format("HP: {0}/{1}", player.HP(), player.MAX_HP());

            //HPシンボル表示変更
            hpui.fillAmount = (float)player.HP() / player.MAX_HP();

            //HPシンボル後追い処理
            if (memo != (float)player.HP() / player.MAX_HP())
            {
                StartCoroutine(Change_Gage(memo, (float)player.HP() / player.MAX_HP()));
            }
            memo = (float)player.HP() / player.MAX_HP();
        }

    }

    void KeyDisplay(int key)
    {
        int v = key;
        for (int i = 0; i < keySprite.Length; i++)
        {
            if (v-- > 0)
            {
                keySprite[i].SetActive(true);
            }
            else
            {
                keySprite[i].SetActive(false);
            }
        }
    }

    IEnumerator Change_Gage(float start, float end)
    {
        float culent = start;
        while(culent > end)
        {
            culent -= 0.005f;
            hpui_m.fillAmount = culent;

            yield return null;
        }
        hpui_m.fillAmount = end;
    }
}
