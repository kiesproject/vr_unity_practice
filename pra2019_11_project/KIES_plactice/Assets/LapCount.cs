using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapCount : MonoBehaviour
{
    int counter = 0;
    public GameObject LapCounter;

    void Update()
    {
        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "stageCollider")
            {
                counter += 1;
            }

            if (this.counter % 6 == 0)
            {
                this.LapCounter.GetComponent<Text>().text = counter.ToString("D1") + "/" + "6";
            }
        }
    }
}
