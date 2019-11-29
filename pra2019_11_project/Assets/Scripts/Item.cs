using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public string NAME = "謎のアイテム";
    public GameObject itemObject;

    public virtual void Naming()
    {
        NAME = "謎のアイテム";
    }

    public virtual void Get_Item()
    {

    }

    public virtual void Create(float luck, float unluck)
    {

    }
}

public class Weapon : Item
{
    public float bulletPowor_p = 1;
    public float fireRate_p = 1;
    
    public override void Naming()
    {
        string a = "";

        if (1.5f < fireRate_p && fireRate_p <= 2.5f)
        {
            a += "改良された";
        } else if (2.5f < fireRate_p)
        {
            a += "魔改造された";
        } else if (0.2f < fireRate_p && fireRate_p <= 0.5f)
        {
            a += "改造に失敗した";
        }else if(fireRate_p < 0.1f)
        {
            a += "壊れた";
        }

        if (1.5f < bulletPowor_p && bulletPowor_p <= 2f)
        {
            a += "良い";
        }else if(2f < bulletPowor_p && bulletPowor_p <= 3.5f)
        {
            a += "すごい";
        }else if(3.5f < bulletPowor_p)
        {
            a += "最高の";
        }else if(0.6f <= bulletPowor_p && bulletPowor_p < 0.8f)
        {
            a += "古い";
        }
        else if (0.1f <= bulletPowor_p && bulletPowor_p < 0.6f)
        {
            a += "悪い";
        }
        else if (bulletPowor_p < 0.05f)
        {
            a += "最悪の";
        }

        NAME = a + "ハンドガン";
    }

    public override void Get_Item()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.Add_Item(this))
            {
                GameManager.instance.luck += 0.2f * (bulletPowor_p + fireRate_p) / 2;
                GameManager.instance.unluck -= 0.2f * (bulletPowor_p + fireRate_p) / 2;
            }
        }
    }

    public override void Create(float luck, float unluck)
    {
        bulletPowor_p = Random.Range(0.7f - luck, 1.3f + unluck);
        fireRate_p = Random.Range(0.7f - luck, 1.3f + unluck);
        Naming();
    }
}

public class Armor : Item
{
    public float add_resistance_p = 1;
    public float add_res_knok_p = 1;
    public float add_speed_p = 1;

    public override void Naming()
    {
        string a = "";
        string s = "";
        if (1.5f <= add_res_knok_p && add_res_knok_p < 3.5f)
        {
            a += "重い";
        }else if(3.5f <= add_res_knok_p)
        {
            a += "不動たる";
        }
        else if (-1.5f <= add_resistance_p && add_resistance_p < 0.0f)
        {
            s = "呪いアーマー";
        }
        else if (-2.5f <= add_resistance_p && add_resistance_p < -1.5f)
        {
            s = "災厄アーマー";
        }
        else if (add_resistance_p < -2.5f)
        {
            s = "滅亡アーマー";
        }


        if (1.5f <= add_speed_p && add_speed_p < 2.5f)
        {
            a += "素早き";
        }
        else if (2.5f <= add_speed_p && add_speed_p < 3.5f)
        {
            a += "俊敏の";
        }else if(3.5f <= add_speed_p)
        {
            a += "神速の";
        }
        else if (-1.5f <= add_speed_p && add_speed_p < 0.0f)
        {
            s = "呪いアーマー";
        }
        else if (-2.5f <= add_speed_p && add_speed_p < -1.5f)
        {
            s = "災厄アーマー";
        }
        else if (add_speed_p < -2.5f)
        {
            s = "滅亡アーマー";
        }


        if (0.0f <= add_resistance_p && add_resistance_p < 1.2f)
        {
            s = "皮アーマー";
        }else if(1.2f <= add_resistance_p && add_resistance_p < 2.5f)
        {
            s = "鉄アーマー";
        }else if(2.5f <= add_resistance_p && add_resistance_p < 3.5f)
        {
            s = "鋼鉄アーマー";
        }else if(3.5 <= add_resistance_p)
        {
            s = "ミスリルアーマー";
        }else if(-1.5f <= add_resistance_p && add_resistance_p < 0.0f)
        {
            s = "呪いアーマー";
        }
        else if (-2.5f <= add_resistance_p && add_resistance_p < -1.5f)
        {
            s = "災厄アーマー";
        }else if(add_resistance_p < -2.5f)
        {
            s = "滅亡アーマー";
        }

        NAME = a + s;
    }

    public override void Create(float luck, float unluck)
    {
        add_resistance_p = Random.Range(0 - luck, 0.6f + unluck);
        add_res_knok_p = Random.Range(0 - luck, 0.6f + unluck);
        add_speed_p = Random.Range(0 - luck, 0.6f + unluck);

        if (add_resistance_p < 0)
        {
            add_res_knok_p += Random.Range(0, Mathf.Abs(add_resistance_p) * (unluck + 1.0f));
            add_speed_p += Random.Range(0, Mathf.Abs(add_resistance_p) * (unluck + 1.0f));
        }

        if (add_res_knok_p < 0)
        {
            add_resistance_p += Random.Range(0, Mathf.Abs(add_res_knok_p) * (unluck + 1.0f));
            add_speed_p += Random.Range(0, Mathf.Abs(add_res_knok_p) * (unluck + 1.0f));
        }

        if (add_speed_p < 0)
        {
            add_res_knok_p += Random.Range(0, Mathf.Abs(add_speed_p) * (unluck + 1.0f));
            add_resistance_p += Random.Range(0, Mathf.Abs(add_speed_p) * (unluck + 1.0f));
        }

        Naming();
    }

    public override void Get_Item()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.Add_Item(this))
            {
                GameManager.instance.luck += 0.2f * (add_resistance_p + add_res_knok_p + add_speed_p) / 3;

            }
        }
    }
}

public class Drink : Item
{
    public int heal = 0;
    public int addResistance = 0;
    public int addSpeed = 0;

    public int time = 0;
    public int timeAddResistance = 0;
    public int timeAddSpeed = 0;

    public override void Naming()
    {
        string s = "";
        string a = "";

        if (heal < -200) { a += "白い"; }
        else if (-200 <= heal && heal < -100) { a += "赤紫色の"; }
        else if (-100 <= heal && heal < -50) { a += "赤い"; }
        else if (-50  <= heal && heal < -25) { a += "橙色の"; }
        else if (-25  <= heal && heal < 0) { a += "黄色い"; }
        else if (0    <= heal && heal < 25) { a += "黄緑色の"; }
        else if (25   <= heal && heal < 50) { a += "緑の"; }
        else if (50   <= heal && heal < 100) { a += "青い"; }
        else if (100  <= heal && heal < 200) { a += "紫の"; }
        else if (200  <= heal) { a += "黒い"; }

        int d = addResistance + timeAddResistance;
        if (d < -15) { a += "すべすべの"; }
        else if(-15 <= d && d < -7) { a += "つるつるな"; }
        else if (-7 <= d && d < 0) { a += "さらさらな"; }
        else if (0 <= d && d < 7) { a += "ざらざらな"; }
        else if (7 <= d && d < 15) { a += "ドロドロな"; }
        else if (15 <= d ) { a += "がらがらの"; }

        d = addSpeed + timeAddSpeed;
        if (d < -15) { a += "臭い"; }
        else if (-15 <= d && d < -7) { a += "生臭い"; }
        else if (-7 <= d && d < 0) { a += "酸っぱい匂いの"; }
        else if (7 <= d && d < 15) { a += "甘い匂いの"; }
        else if (15 <= d) { a += "甘ったるい"; }

        d = time;
        if (10 <= d && d < 20) { s = "上薬"; }
        else if (20 <= d) { s= "極薬"; }
        else { s = "薬"; }

        NAME = a + s;
    }

    public override void Create(float luck, float unluck)
    {
        heal = Random.Range(-30 - (int)(luck * 30), 100 + (int)(unluck * 40));
        addResistance = Random.Range(0 - (int)(luck * 10), 3 + (int)(unluck * 10));
        addSpeed = Random.Range(0 - (int)(luck * 5), 3 + (int)(unluck * 10));
        timeAddResistance = Random.Range(0 - (int)(luck * 5), 3 + (int)(unluck * 10));
        timeAddSpeed = Random.Range(0 - (int)(luck * 5), 3 + (int)(unluck * 10));
        time = Random.Range(2 - (int)(luck * 5), 10 + (int)(unluck * 10));

        Naming();
    }

    public override void Get_Item()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.Add_Item(this))
            {
                float h = heal / 200;
                float adr = addResistance / 20;
                float ads = addSpeed / 20;
                float t = time / 20;
                float tar = timeAddResistance / 20;
                float tas = timeAddSpeed / 20;
                float l = (h + adr + ads + t + tar + tas) / 5;

                GameManager.instance.luck += l;
            }
        }
    }
}

public class Gold : Item
{
    public int gold = 0;
    public float addLuck = 0;

    public override void Naming()
    {
        NAME = gold.ToString() + "G";
    }

    public override void Create(float luck, float unluck)
    {
        gold = Random.Range(10 - (int)luck * 30, 100 + (int)unluck * 30);
        if (gold < 0) { addLuck = gold / 80; gold = 1; }
    }

    public override void Get_Item()
    {
        GameManager.instance.golds += gold;

        if (addLuck < 0)
        {
            GameManager.instance.unluck += addLuck;
        }
        else
        {
            GameManager.instance.luck += addLuck;
        }
    }
}

public class Key : Item
{
    public override void Get_Item()
    {
        GameManager.instance.Add_Key(1);
    }
}

public class Bullet : Item
{
    public int number = 10;

    public override void Get_Item()
    {
        GameManager.instance.player.bullets += number;
    }
}

public class Map : Item
{
    public override void Get_Item()
    {
        GameManager.instance.mapSystem.Set_MapRoomAll();
    }
}
