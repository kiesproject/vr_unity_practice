using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //private Vector3 position;
    //private Vector3 screenWorldPos;
    public GameObject playerObj;
    private Vector3 playerPos;

    private float dumpRotateZ;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        playerPos = playerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += playerObj.transform.position - playerPos;
        playerPos = playerObj.transform.position;

        // マウスの右クリックを押している間
        // マウスの移動量
        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");
        // playerの位置のY軸を中心に、回転（公転）する
        transform.RotateAround(playerPos, Vector3.up, mouseInputX * Time.deltaTime * 200f);

        // カメラの垂直移動（※角度制限なし、必要が無ければコメントアウト）
        transform.RotateAround(playerPos, transform.right, mouseInputY * Time.deltaTime * 200f);

        /*
        position = Input.mousePosition;
        position.z = 10f;
        this.transform.LookAt(position);
        */

    }
}
