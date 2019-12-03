using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugGUIBottan : MonoBehaviour
{
    public GameObject Generate;
    // Start is called before the first frame update
    public void OnClick()
    {
        Generate.GetComponent<MeiroGeneraterSquare>().GeneratPublic();
        GameManager.instance.SetPlayerKoudou(4);

    }
}
