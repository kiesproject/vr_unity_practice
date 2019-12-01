using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SShoot : MonoBehaviour
{

    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public Transform barrelLocation;
    public Transform casingExitLocation;

    private Vector3 target;

    public float shotPower = 100f;

    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;
    }

    void Update()
    { 

    }

    public void ShootFire(Vector3 target)
    {
        this.target = target;
        barrelLocation.LookAt(target);


        var anim = GetComponent<Animator>();
        if (GameManager.instance.player.weapon != null)
        {
            anim.speed = GameManager.instance.player.weapon.fireRate_p;
        }
        anim.SetTrigger("Fire");
        
    }

    void Shoot()
    {
        //  GameObject bullet;
        //  bullet = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        // bullet.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        GameObject tempFlash;
        if (GameManager.instance.player.bullets > 0)
        {
            GameManager.instance.player.bullets--;
            var o = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
            AttackBullet ab = o.GetComponent<AttackBullet>();
            float d = 0;
            if (GameManager.instance.player.weapon != null) d = GameManager.instance.player.weapon.bulletPowor_p * ab.damege;
            ab.damege += Mathf.RoundToInt(d);
            o.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
        }
       // Destroy(tempFlash, 0.5f);
        //  Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation).GetComponent<Rigidbody>().AddForce(casingExitLocation.right * 100f);
       
    }

    void CasingRelease()
    {
        if (GameManager.instance.player.bullets > 0)
        {
            GameObject casing;
            casing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
            casing.GetComponent<Rigidbody>().AddExplosionForce(550f, (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
            casing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(10f, 1000f)), ForceMode.Impulse);
        }
        }


}
