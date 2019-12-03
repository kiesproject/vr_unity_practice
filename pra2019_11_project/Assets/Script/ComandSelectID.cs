using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComandSelectID : MonoBehaviour
{
    public GameObject[] Comand=new GameObject[6];
    private int ComandID = 0;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < Comand.Length; i++)
        {
            if (Comand[i].activeSelf)
            {
                ComandID = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ComandUpdate()
    {
        for (int i = 0; i < Comand.Length; i++)
        {
            if (Comand[i].activeSelf)
            {
                ComandID = i;
            }
        }
    }

    public int GetComandID()
    {
        return ComandID;
    }

}
