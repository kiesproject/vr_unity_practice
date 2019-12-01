using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootController : MonoBehaviour
{
    Camera _camera;
    public GameObject hand;
    public GameObject test;

    public SShoot sShoot;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameManager.State.GAME)
        {

            if (_camera != null)
            {
                var pos = ScreenToWorld2();
                hand.transform.LookAt(pos);

                if (Input.GetMouseButtonDown(0) && !GameManager.instance.cursorOnUI)
                {
                    sShoot.ShootFire(pos);
                }
            }
        }
    }

    private Vector3 ScreenToWorld(float distance)
    {
        var screenPos = Input.mousePosition;
        // zの値をスクリーンの位置(カメラからz離れた位置)
        screenPos.z = distance;
        // スクリーン座標をワールド座標に変換
        return _camera.ScreenToWorldPoint(screenPos);
            
    }

    private Vector3 ScreenToWorld2()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();

        var pos = ScreenToWorld(2);
        if (Physics.Raycast(ray, out hit))
        {
            pos = hit.point;
            
        }

        test.transform.position = pos;
        return pos;
    }
}
