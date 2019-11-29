using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugIf : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        uint mask = 0b_000;
        mask+= 0b_100;
        mask += 0b_001;
        const uint maskRL = 0b_101;
        const uint maskR = 0b_001;
        if ((mask & maskRL) == maskRL)
        {
            Debug.Log(mask);
            Debug.Log("ifIn");
        }
        else if ((mask & maskR) == maskR)
        {
            Debug.Log(mask);
            Debug.Log("ifIn");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
