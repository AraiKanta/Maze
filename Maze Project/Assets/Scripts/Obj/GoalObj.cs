using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゴールの座標を他のオブジェクトに渡したりするクラス
/// </summary>
public class GoalObj : MonoBehaviour
{
    /// <summary> 鍵オブジェクトの変数 </summary>
    [Header("鍵の座標(KeyObjPrefabが入る)")]
    [SerializeField] private Transform m_keyObj = null;
    /// <summary> 鍵オブジェクトの座標を入れる変数 </summary>
    private Transform m_keyObjPos = null;

    void Start()
    {
        FindKey();
    }

    /// <summary> 鍵の座標をセットしている </summary>
    public void SetKey(Transform key)
    {
        m_keyObj= key;
    }

    /// <summary>
    /// 鍵を見つけて座標を渡しているメソッド
    /// </summary>
    public void FindKey()
    {
        //鍵オブジェクトを見つけて座標を渡してる
        m_keyObj = FindObjectOfType<GetKey>().transform;
        m_keyObj.GetComponent<GetKey>().SetGoal(transform);

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

    public void ActiveObj()
    {
        this.gameObject.SetActive(true);
    }
}
