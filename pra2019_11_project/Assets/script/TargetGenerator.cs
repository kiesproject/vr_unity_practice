using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour
{
    [SerializeField] GameObject Target;
    [SerializeField] int TargetCount = 5;

    private GameObject[] Points;

    // Start is called before the first frame update
    void Start()
    {
        Points = GameObject.FindGameObjectsWithTag("Point");
        List<int> indexs = new List<int>();

        int index;

        for (int i = 0; i < TargetCount; i++)
        {
            while (true)
            {
                index = Random.Range(0, Points.Length);
                if (indexs.IndexOf(index) == -1)
                {
                    indexs.Add(index);
                    break;
                }
            }
            Instantiate(Target, Points[index].transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
