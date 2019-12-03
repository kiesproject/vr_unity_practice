using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespon : MonoBehaviour
{
    private GameObject[] enemyObject;
    private int[][] spaceInt;
    // Start is called before the first frame update
    void Start()
    {
        enemyObject = GameManager.instance.GetEnemyObject();
        spaceInt = GameManager.instance.GetSpace();
    }

    // Update is called once per frame
    void Update()
    {
        int enemyNum = spaceInt.Length;
        for(int i = 0; i < enemyNum; i++)
        {
            int[] space = spaceInt[Random.Range(0, spaceInt.Length)];
            int spaceIndex = space[Random.Range(0, space.Length)];
            int x = spaceIndex % GameManager.instance.GetMeiroNeighborhood();
            int z = spaceIndex / GameManager.instance.GetMeiroNeighborhood();
            GameObject cloneObject=GameObject.Instantiate (enemyObject[Random.Range(0,enemyObject.Length)], new Vector3(x*2, 0, z*2), Quaternion.identity);
            cloneObject.GetComponent<MeiroObjectID>().SetID(spaceIndex);
            cloneObject.GetComponent<MeiroObjectID>().SetMeiroID(6);
        }
    }
}
