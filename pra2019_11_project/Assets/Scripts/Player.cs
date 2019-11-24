using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private MyCamera myCamera;

    float horizontal = 0;
    float vertical = 0;
    float coefficient = 10;

    Rigidbody rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        if (myCamera != null)
        {
            Vector3 dirMove = myCamera.Get_MoveForce(vertical, horizontal);

            rig.AddForce(coefficient * dirMove - rig.velocity);
        }
    }

    public void Telport(Vector3 poss)
    {
        transform.position = poss;
    }
}
