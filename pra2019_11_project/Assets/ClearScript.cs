using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Color color = gameObject.GetComponent<Renderer>().material.color;
        color.a = 0.9f;

        gameObject.GetComponent<Renderer>().material.color = color;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
