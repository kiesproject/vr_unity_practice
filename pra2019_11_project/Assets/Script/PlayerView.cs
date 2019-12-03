using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public GameObject Player;
    public Transform PlayerT;
    public float Ydistance=0;
    public float Zdistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = PlayerT.position + new Vector3(0, Ydistance, Zdistance); 
    }
}
