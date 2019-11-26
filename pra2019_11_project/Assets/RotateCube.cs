using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{

    public float rotateSpeed = 1.0f;          // 回転速度
    public float rotateDuration = 1.0f;       // 回転の継続時間
    public float rotateDirection = 0;         // 回転方向　0…左　1…右　消す可能性アリ

    float rotateElapsedTime = 0;              // 回転経過時間

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, 0, 5));

        // transform.Rotate(new Vector3(0, 0, 5));

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.Rotate(new Vector3(0, 0, 5));
        }

    }
}
