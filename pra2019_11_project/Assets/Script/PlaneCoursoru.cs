using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneCoursoru : MonoBehaviour
{
    public GameObject Coursoru;
    public GameObject AttackCoursoru;
    private bool coursoru=false,attackCoursoru=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coursoru||attackCoursoru)
        {
            Coursoru.SetActive(coursoru);
            AttackCoursoru.SetActive(attackCoursoru);
        }
        else
        {
            Coursoru.SetActive(coursoru);
            AttackCoursoru.SetActive(attackCoursoru);
        }
        coursoru = false;
        attackCoursoru = false;
    }

    public void SetPointCoursoru()
    {
        coursoru = true;
    }
    public void SetAttackCoursoru()
    {
        attackCoursoru = true;
    }
}
