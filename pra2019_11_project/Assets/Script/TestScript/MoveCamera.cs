using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        Transform myTransform = this.transform;

        Vector3 pos = myTransform.position;
        if (move == 1)
        {
            pos.x += 0.1f;
        }
        else if(move == -1)
        {
            pos.x += -0.1f;
        }

        myTransform.position = pos;
    }
}
