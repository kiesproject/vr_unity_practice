using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    public GameObject Enemy;
    public float time = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Vector3 CreatePoint = new Vector3(0, 0, 0);
            Instantiate(Enemy, CreatePoint, Quaternion.identity);
            time = 30;
        }
    }
}
