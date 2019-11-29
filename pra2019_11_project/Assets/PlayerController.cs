using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    // Start is called before the first frame update
    void Start()
    {
        SetNowIndex();
        GetComand();
        GetComandTransform();
        Map();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Koudou()
    {
        switch (GameManager.instance.GetPlayerKoudou())
        {
            case 0:
                
                break;

            case 1:
                Move();
                GameManager.instance.SetPlayerKoudou(0);
                break;
            case 2:
                SelectComand();
                GameManager.instance.SetPlayerKoudou(0);
                break;
        }
    }

}
