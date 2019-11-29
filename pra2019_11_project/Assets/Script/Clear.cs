using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    public GameObject Text;

    // Use this for initialization
    void Start()
    {

        Text.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    // オブジェクトに触れたらText表示する
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Text.SetActive(true);
        }
    }
}
