using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (pos.z > 10)
        {
            Application.LoadLevel("Stage1");
        }
    }
}
