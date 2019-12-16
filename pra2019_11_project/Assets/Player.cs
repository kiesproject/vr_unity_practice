using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //*** ==================
    //*** 非常に良いです。
    //*** ==================

    //public Transform target;
    //public Vector3[] targetPosition;
    //public Vector3 nowTargetPosition;

    float rotateY;
    Maze2 maze;

    // Start is called before the first frame update
    void Start()
    {
        maze = new Maze2();
        this.transform.position = new Vector3(-2.5f * ((float)maze.maze_w - 1.0f), this.transform.position.y, 2.5f * ((float)maze.maze_h - 1.0f));

        //targetPosition = new Vector3[4];
        //ReDefinition();
        //nowTargetPosition = targetPosition[0];

        Vector3 north = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 west = new Vector3(0.0f, 90.0f, 0.0f);
        Vector3 south = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 east = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (this.transform.localEulerAngles.y == 0.0f)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 5.0f);
            }
            if (this.transform.localEulerAngles.y == 90.0f)
            {
                this.transform.position = new Vector3(this.transform.position.x + 5.0f, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.localEulerAngles.y == 180.0f || this.transform.localEulerAngles.y == -180.0f)
            {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 5.0f);
            }
            if (this.transform.localEulerAngles.y == -90.0f)
            {
                this.transform.position = new Vector3(this.transform.position.x - 5.0f, this.transform.position.y, this.transform.position.z);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotateY += 90.0f;
            this.transform.rotation = Quaternion.AngleAxis(rotateY, new Vector3(0.0f, 1.0f, 0.0f));
            //ReDefinition();
            //if (nowTargetPosition == targetPosition[0])
            //{
            //Quaternion targetRotation = Quaternion.LookRotation(targetPosition[1] - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            //}
            //if (nowTargetPosition == targetPosition[1])
            //{
            //Quaternion targetRotation = Quaternion.LookRotation(targetPosition[2] - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            //}
            //if (nowTargetPosition == targetPosition[2])
            //{
            //Quaternion targetRotation = Quaternion.LookRotation(targetPosition[3] - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            //}
            //if (nowTargetPosition == targetPosition[3])
            //{
            //Quaternion targetRotation = Quaternion.LookRotation(targetPosition[0] - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            //}
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotateY -= 90.0f;
            this.transform.rotation = Quaternion.AngleAxis(rotateY, new Vector3(0.0f, 1.0f, 0.0f));
            //ReDefinition();
            //if (nowTargetPosition == targetPosition[0])
            //{
            //Quaternion targetRotation = Quaternion.LookRotation(targetPosition[3] - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            //}
            //if (nowTargetPosition == targetPosition[1])
            //{
            //Quaternion targetRotation = Quaternion.LookRotation(targetPosition[0] - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            //}
            //if (nowTargetPosition == targetPosition[2])
            //{
            //Quaternion targetRotation = Quaternion.LookRotation(targetPosition[1] - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            //}
            //if (nowTargetPosition == targetPosition[3])
            //{
            //Quaternion targetRotation = Quaternion.LookRotation(targetPosition[2] - transform.position);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
            //}
        }
    }

    //void ReDefinition()
    //{
    //targetPosition[0] = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 5.0f);
    //targetPosition[1] = new Vector3(this.transform.position.x + 5.0f, this.transform.position.y, this.transform.position.z);
    //targetPosition[2] = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 5.0f);
    //targetPosition[3] = new Vector3(this.transform.position.x - 5.0f, this.transform.position.y, this.transform.position.z);
    //}
}
