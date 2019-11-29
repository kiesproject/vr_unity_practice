using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public Text clear;
    public Text gameover;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Playerded()
    {
        gameover.enabled = true;
    }
    public void Clear()
    {
        clear.enabled = true;
    }
}
