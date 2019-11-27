using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    Camera _camera;
    public GameObject hand;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_camera != null)
        {
            var pos = ScreenToWorld(2);
            hand.transform.LookAt(pos);
        }
    }

    private Vector3 ScreenToWorld(float distance)
    {
        var screenPos = Input.mousePosition;
        // zの値をスクリーンの位置(カメラからz離れた位置)
        screenPos.z = distance;
        // スクリーン座標をワールド座標に変換
        return _camera.ScreenToWorldPoint(screenPos + hand.transform.localPosition);
            
    }
}
