using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntoroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameScript gameScript;
    public float speed = 20;
    public float jumpspeed = 50;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = new Vector3(moveHorizontal, 0, moveVertical);
        if (Input.GetButtonDown("Jump"))
        {
            movement.y = jumpspeed;
        }
        rb.AddForce(movement * speed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("衝突したオブジェクト:" + gameObject.name);
            Debug.Log("衝突されたオブジェクト:" + collision.gameObject.name);
            gameScript.Playerded();
            Destroy(gameObject);
        }
    }
}
