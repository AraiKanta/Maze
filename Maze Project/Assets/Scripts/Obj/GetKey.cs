using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 鍵を取ったときゴールを出現させるクラス </summary>
public class GetKey : MonoBehaviour
{
    /// <summary> 鍵オブジェクトの変数 </summary>
    private GameObject m_key = null;
    /// <summary> ゴールの座標 </summary>
    [Header("ゴールの座標(GoalObjPrefabが入る)")]
    [SerializeField] Transform m_goalObj;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            m_key.SetActive(false);

            m_goalObj.gameObject.SetActive(true);
        }
    }
}