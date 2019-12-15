using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    Vector3 doorPos;
    Transform wallTransform;
    [SerializeField]string colisionObj;

    void OnTriggerEnter(Collider collider)
    {
        wallTransform = this.transform;
        Vector3 doorPos = this.transform.position;
        colisionObj = collider.gameObject.name;
        if (colisionObj == "Player")
        {
            doorPos.z += 1.48f;
            wallTransform.position = doorPos;
        }
        
    }
    void OnTriggerExit(Collider collider)
    {
        wallTransform = this.transform;
        Vector3 doorPos = this.transform.position;
        colisionObj = collider.gameObject.name;
        if(colisionObj == "Player")
        {
            doorPos.z -= 1.48f;
            wallTransform.position = doorPos;
        }
    }
}
