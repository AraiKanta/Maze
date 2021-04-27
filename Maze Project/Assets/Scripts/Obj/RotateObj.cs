using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
    /// <summary> カメラのY軸に対して加える値 </summary>
    [Header("カメラのY軸に対して加える値")]
    [SerializeField] float m_vec3Y = 1f;
    /// <summary> 回転スピード </summary>
    [Header("回転スピード")]
    [SerializeField] float m_speed = 1f;

    void Update()
    {
        //オブジェクトを回転させている
        transform.RotateAround(transform.position, new Vector3(0, m_vec3Y, 0), m_speed);
    }
}
