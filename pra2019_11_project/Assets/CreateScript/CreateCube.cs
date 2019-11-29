using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCube : MonoBehaviour
{

    public GameObject originalObject;
    public float timeOut = 3.0f;
    public float Nowtime;
    public float z = 0.75f;

    // Use this for initialization
    void Start()
    {
        Nowtime = 0f;
    }

    // Update is called once per frame
    //cubeをランダムな位置に一定時間ごとに生成する
    void Update()
    {
        Nowtime += Time.deltaTime;
        if (Nowtime >= timeOut)
        {
            GameObject cube = Instantiate(originalObject) as GameObject;
            cube.transform.position = new Vector3(Random.Range(-10, 10), z, Random.Range(-10, 10));
            Nowtime = 0.0f;
        }       
    }
}


