using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTest : MonoBehaviour
{

    public GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.transform);

        gameObject.transform.Translate(0,0,0.05f);
    }
}
