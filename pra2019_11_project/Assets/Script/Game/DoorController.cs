using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    Vector3 doorPos;
    Transform wallTransform;
    [SerializeField]string colisionObj;

    void Start()
    {
        
    }

    void Update()
    {
       
    }

    void OnTriggerEnter(Collider collider)
    {
        wallTransform = this.transform;
        Vector3 doorPos = this.transform.position;
        colisionObj = collider.gameObject.name;
        if (colisionObj == "Player")
        {
            doorPos.z += 1.48f;
            wallTransform.position = doorPos;
            Debug.Log("入った");
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
            Debug.Log("出た");
        }
    }
}
