using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    [SerializeField]
    Transform childTarget;

    [SerializeField]
    Transform cameraH;

    Vector2 _prevMousePos = new Vector3();
    const float MaxAngleX = 60f;
    const float MinAngleX = -60f;
    const float RotateSpeedH = 270.0f;
    const float RotateSpeedV = 70.0f;

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //StartCoroutine(CursorSwitch());
        //Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SetCursorPos(0, 0);
        }

        CameraMove();
    }

    /// <summary>
    /// カメラの方向による移動ベクトルを取得する
    /// </summary>
    /// <param name="hor"></param>
    /// <param name="ver"></param>
    /// <returns></returns>
    public Vector3 Get_MoveForce(float hor, float ver)
    {
        var cameraForward = Vector3.Scale(transform.forward, new Vector3(1, 0, 1)).normalized;
        var moveForward = cameraForward * hor + transform.right * ver;
        return moveForward;
    }   

    /// <summary>
    /// カメラ操作を行うメソッド
    /// </summary>
    private void CameraMove()
    {
        Vector2 mousePos = Input.mousePosition;

        if (Input.GetMouseButton(1))
        {
            //カーソルを動かした
            if (_prevMousePos != mousePos)
            {
                float angleH = (mousePos.x - _prevMousePos.x) / Screen.width * RotateSpeedH;
                float angleV = (_prevMousePos.y - mousePos.y) / Screen.height * RotateSpeedV;
                //sDebug.Log(string.Format("angleH: {0} angleV: {1}", angleH, angleV));

                transform.localEulerAngles = RotateV(transform.localEulerAngles, angleV);
                cameraH.transform.localEulerAngles = RotateH(cameraH.transform.localEulerAngles, angleH);
            }
        }
        _prevMousePos = Input.mousePosition;
    }

    private void CameraMove2()
    {
        float sensitivity = 1.0f; // いわゆるマウス感度
        float mouse_move_x = Input.GetAxis("Mouse X") * sensitivity;
        float mouse_move_y = Input.GetAxis("Mouse Y") * sensitivity;
        Debug.Log(string.Format("mouseX: {0} MouseY: {1}", mouse_move_x, mouse_move_y));

        Vector2 mousePos = Input.mousePosition;

        //if (Input.GetMouseButton(1))
        {
            //カーソルを動かした
            if (_prevMousePos != mousePos)
            {
                float angleH = mouse_move_x;
                float angleV = mouse_move_y;

                Debug.Log(string.Format("angleH: {0} angleV: {1}", angleH, angleV));

                transform.localEulerAngles = RotateV(transform.localEulerAngles, angleV);
                cameraH.transform.localEulerAngles = RotateH(cameraH.transform.localEulerAngles, angleH);
            }
        }
        _prevMousePos = Input.mousePosition;
    }

    Vector3 RotateH(Vector3 eulerAngles, float angle)
    {
        eulerAngles.y += angle;
        return eulerAngles;
    }

    Vector3 RotateV(Vector3 eulerAngles, float angle)
    {
        if (eulerAngles.x > 180f) eulerAngles.x -= 360f;
        eulerAngles.x = Mathf.Clamp(eulerAngles.x + angle, MinAngleX, MaxAngleX);
        if (eulerAngles.x < 0f) eulerAngles.x += 360f;
        return eulerAngles;
    }
}
