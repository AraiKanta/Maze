using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Playerを制御するクラス </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerContoller : MonoBehaviour
{
    /// <summary> 移動用のJoysitck(画面左) </summary>
    [Header("移動用のJoysitck(画面左)")]
    [SerializeField] private FloatingJoystick m_floatingJoystickMove = null;
    /// <summary> 視点変更用のJoysitck(画面右) </summary>
    [Header("視点変更用のJoysitck(画面右)")]
    [SerializeField] private FloatingJoystick m_floatingJoystickCamera = null;
    /// <summary> 参照しているクラスの変数 </summary>
    private CharacterController m_characterController = null;
    /// <summary> プレイヤーのスピード </summary>
    [Header("プレイヤーのスピード")]
    [SerializeField] private float m_speed = 1f;
    /// <summary> カメラの回転速度 </summary>
    [Header("カメラの回転速度")]
    [SerializeField] private float m_rotateCamera = 1f;
    /// <summary> カメラの親Object </summary>
    [Header("カメラの親Object")]
    [SerializeField] private Transform m_camera = null;
    /// <summary> カメラの座標 </summary>
    private Transform m_cameraPos = null;
    /// <summary> カメラの向いている方向 </summary>
    private Vector3 m_cameraForward;
    /// <summary> プレイヤーの移動 </summary>
    private Vector3 m_direction;
    /// <summary> プレイヤーの方向 </summary>
    private Vector3 m_velocity;
    /// <summary> GameManagerクラスを参照している変数 </summary>
    GameManager m_gameManager = null;
    /// <summary> アニメーターの変数 </summary>
    Animator m_anim = null;
    /// <summary> 鍵オブジェクトの変数 </summary>
    [Header("鍵の座標(KeyObjPrefabが入る)")]
    [SerializeField] private Transform m_keyObj = null;
    /// <summary> 鍵オブジェクトの座標を入れる変数 </summary>
    private Transform m_keyObjPos = null;

    private void Start()
    {
        m_characterController = GetComponent<CharacterController>();
        m_anim = GetComponent<Animator>();

        //カメラを見つけて座標を渡している
        m_camera = GameObject.Find("Camera").transform;
        m_camera.GetComponent<CameraManager>().SetPlayer(transform);

        ////鍵オブジェクトを見つけて座標を渡してる
        m_keyObj = FindObjectOfType<GetKey>().transform;
        m_keyObj.GetComponent<GetKey>().SetPlayer(transform);

        //カメラ見つける
        if (Camera.main != null)
        {
            m_cameraPos = Camera.main.transform;
        }
        else
        {
            Debug.LogError("カメラついてないからみつからない。カメラが必要です");
        }

        //鍵をみつける
        if (m_keyObj != null)
        {
            m_keyObjPos = m_keyObj.transform;
        }
        else
        {
            Debug.Log("鍵見つからないよ");
        }
    }

    private void Update()
    {
        PlayerMove();
    }

    /// <summary>
    /// プレイヤーの動きを制御する関数
    /// </summary>
    public void PlayerMove() 
    {
        //左スティックでプレイヤーの移動
        m_direction = Quaternion.Euler(0, m_camera.eulerAngles.y, 0) * (Vector3.forward * m_floatingJoystickMove.Vertical + Vector3.right * m_floatingJoystickMove.Horizontal);
        if (m_direction != new Vector3(0, 0, 0))
        {
            transform.localRotation = Quaternion.LookRotation(m_direction);

            //animatorのパラメーターの判定
            m_anim.SetBool("Run", true);
        }
        else
        {
            //animatorのパラメーターの判定
            m_anim.SetBool("Run", false);
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

    /// <summary>
    /// ゴール判定と鍵を拾った判定
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter(Collider collider)
    {
        //このタブのObjectのコライダーに触れたらゴール
        if (collider.gameObject.tag == "GoalObj")
        {
            Debug.Log("ゴール");

            //GameManagerにクリアを知らせる(Finished関数)
            m_gameManager = GameObject.FindObjectOfType<GameManager>();
            if (m_gameManager)
            {
                m_gameManager.Finished();
            }
        }
    }
}
