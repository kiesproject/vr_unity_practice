using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCoursoru : MonoBehaviour
{
    public GameObject Coursoru;
    private bool coursoru=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coursoru)
        {
            Coursoru.SetActive(true);
        }
        else
        {
            Coursoru.SetActive(false);
        }
        coursoru = false;
    }

    public void SetPointCoursoru()
    {
        coursoru = true;
    }
}
