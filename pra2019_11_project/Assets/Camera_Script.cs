using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    //　メインカメラ
    [SerializeField]
    private GameObject mainCamera;
    //　切り替える他のカメラ
    [SerializeField]
    private GameObject otherCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        // 右、左を超えたら位置を切り替え、戻るとまた切り替える
        if (pos.x > 7)
        {
            //mainCamera.SetActive(!mainCamera.activeSelf);
            otherCamera.SetActive(!otherCamera.activeSelf);
        }
    }
}
