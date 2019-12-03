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
        Koudou();
    }

    private void Koudou()
    {
        switch (GameManager.instance.GetPlayerKoudou())
        {
            case 0:
                
                break;

            case 1:
                Move();
                break;
            case 2:
                SelectComand();
                GameManager.instance.SetPlayerKoudou(0);
                break;
            case 3:
                SetNowIndex();
                Map();
                GameManager.instance.SetPlayerKoudou(0);
                break;
            case 4:
                SetNowIndex();
                GetComand();
                GetComandTransform();
                Map();
                GameManager.instance.SetPlayerKoudou(0);
                break;
        }
    }

}
