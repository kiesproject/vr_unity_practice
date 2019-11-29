using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotate: MonoBehaviour
{
    void Update()
    {
        //z軸に3回す
        transform.Rotate(new Vector3(0, 0, 3));
    }
}
