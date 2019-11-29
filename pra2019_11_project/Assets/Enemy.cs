using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{

    GameObject Player;
    GameObject homingObj;
    public float Speed;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("nige");
        homingObj = GameObject.Find("oni");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(Player.transform.position.x, Player.transform.position.y, Player.transform.position.z), 9.0f * Time.deltaTime);
    }
}