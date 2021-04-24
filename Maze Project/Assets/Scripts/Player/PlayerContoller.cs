using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Playerを制御するクラス </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerContoller : MonoBehaviour
{
    /// <summary> 移動用のJoysitck(画面左) </summary>
    [Header("移動用のJoysitck(画面左)")]
    [SerializeField] FloatingJoystick m_floatingJoystickMove = null;
    /// <summary> 視点変更用のJoysitck(画面右) </summary>
    [Header("視点変更用のJoysitck(画面右)")]
    [SerializeField] FloatingJoystick m_floatingJoystickCamera = null;
    /// <summary> 参照しているクラスの変数 </summary>
    private CharacterController m_characterController = null;
    /// <summary> プレイヤーのスピード </summary>
    [Header("プレイヤーのスピード")]
    [SerializeField] float m_speed = 1f;
    /// <summary> カメラの回転速度 </summary>
    [Header("カメラの回転速度")]
    [SerializeField] float m_rotateCamera = 1f;
    /// <summary> カメラの親Object </summary>
    [Header("カメラの親Object")]
    [SerializeField] Transform m_camera = null;
    /// <summary> カメラの座標 </summary>
    private Transform m_cameraPos = null;
    /// <summary> カメラの向いている方向 </summary>
    private Vector3 m_cameraForward;
    /// <summary> プレイヤーの移動 </summary>
    private Vector3 m_direction;
    /// <summary> プレイヤーの方向 </summary>
    private Vector3 m_velocity;

    private void Start()
    {
        m_characterController = GetComponent<CharacterController>();

        //カメラを見つけて座標を渡している
        m_camera = GameObject.Find("Camera").transform;
        m_camera.GetComponent<CameraManager>().SetPlayer(transform);

        //カメラ見つける
        if (Camera.main != null)
        {
            m_cameraPos = Camera.main.transform;
        }
        else
        {
            Debug.LogError("カメラついてないからみつからないよ～ん。カメラがひつようだよ～ん");
        }
    }

    private void Update()
    {
        //左スティックでプレイヤーの移動
        m_direction = Quaternion.Euler(0, m_camera.eulerAngles.y, 0) * (Vector3.forward * m_floatingJoystickMove.Vertical + Vector3.right * m_floatingJoystickMove.Horizontal);
        if (m_direction != new Vector3(0, 0, 0))
        {
            transform.localRotation = Quaternion.LookRotation(m_direction);
        }

        //右スティックでカメラの回転
        m_cameraForward = Vector3.forward * m_floatingJoystickCamera.Vertical + Vector3.right * m_floatingJoystickCamera.Horizontal;
        if (m_cameraForward != new Vector3(0, 0, 0))
        {
            //カメラの回転
            float speed = m_floatingJoystickCamera.Horizontal * Time.deltaTime * m_rotateCamera;
            m_camera.eulerAngles += new Vector3(0, speed, 0);
        }

        //プレイヤーの移動
        transform.position = new Vector3(transform.position.x + m_velocity.x, 0, transform.position.z + m_velocity.z);

        m_characterController.Move(m_direction * m_speed * Time.deltaTime);
    }
}
