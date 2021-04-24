using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    /// <summary> プレイヤーの座標 </summary>
    [SerializeField] Transform m_player;
    [SerializeField] float smoothTime = 1;
    /// <summary> 保管用 </summary>
    Vector3 m_velocity;

    /// <summary> プレイヤーの座標をセットしている </summary>
    public void SetPlayer(Transform player)
    {
        m_player = player;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, m_player.position, ref m_velocity, smoothTime);
    }
}
