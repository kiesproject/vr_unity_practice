using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDel : MonoBehaviour
{
    [SerializeField] float LifeTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime < 0)
        {
            Destroy(gameObject);
        }
    }
}
