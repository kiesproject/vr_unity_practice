using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCase : MonoBehaviour
{
    public float second = 5;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }
}
