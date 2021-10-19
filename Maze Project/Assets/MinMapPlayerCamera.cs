using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> ミニマップ用のプレイヤーCameraのクラス </summary>
public class MinMapPlayerCamera : MonoBehaviour
{
    /// <summary> プレイヤーの座標 </summary>
    [Header("プレイヤーの座標(PlayerPrefabを入れる)")]
    [SerializeField] private Transform m_player;

    /// <summary> プレイヤーの座標をセットしている </summary>
    public void SetPlayer(Transform player)
    {
        m_player = player;
    }


}
