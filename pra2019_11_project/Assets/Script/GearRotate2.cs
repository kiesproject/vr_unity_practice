using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotate2 : MonoBehaviour
{
    void Update()
    {
        //z軸にー3回す
        transform.Rotate(new Vector3(0, 0, -3));
    }
}
