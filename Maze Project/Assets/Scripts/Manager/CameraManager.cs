using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    /// <summary> プレイヤーの座標 </summary>
    [Header("プレイヤーの座標(PlayerPrefabを入れる)")]
    [SerializeField] Transform m_player;
    /// <summary> プレイヤーの周りを動く時の時間 </summary>
    [Header("プレイヤーの周りを動く時の時間")]
    [SerializeField] float smoothTime = 1;
    /// <summary> 値保管用 </summary>
    Vector3 m_velocity;

    /// <summary> プレイヤーの座標をセットしている </summary>
    public void SetPlayer(Transform player)
    {
        m_player = player;
    }

    void Update()
    {
        //カメラがプレイヤー周りを動く時の処理
        transform.position = Vector3.SmoothDamp(transform.position, m_player.position, ref m_velocity, smoothTime);
    }
}
