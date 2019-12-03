using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiroObjectID : MonoBehaviour
{
    [SerializeField]
    private int ID;
    [SerializeField]
    private int MeiroID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetID(int id)
    {
        ID = id;
    }

    public int GetID()
    {
        return ID;
    }

    public void SetMeiroID(int id)
    {
        MeiroID = id;
    }

    public int GetMeiroID()
    {
        return MeiroID;
    }
}
