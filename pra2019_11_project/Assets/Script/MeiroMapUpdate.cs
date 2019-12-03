using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeiroMapUpdate : MonoBehaviour
{
    private int[] meiroMapData;
    private int playerNextIndex = -1;
    private int[] enemyNextIndex; 
    // Start is called before the first frame update
    void Start()
    {
        meiroMapData = GameManager.instance.GetMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
