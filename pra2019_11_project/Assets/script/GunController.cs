using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] Camera MainCamera;

    [SerializeField] GameObject Gun;
    [SerializeField] float Range = 100.0f;

    [SerializeField] GameObject Bullet;
    [SerializeField] Transform Muzzle;
    [SerializeField] float BulletSpeed = 100.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit;

        if (Physics.Raycast(MainCamera.transform.position, MainCamera.transform.forward, out raycastHit, Range))
        {
            Gun.transform.LookAt(raycastHit.point);
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject @object = Instantiate(Bullet, Muzzle.position, Quaternion.identity);
            @object.GetComponent<Rigidbody>().AddForce(Muzzle.forward * BulletSpeed, ForceMode.Impulse);
        }
    }
}
