using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

// TODO:実装途中
/// <summary> 鍵を取ったときゴールを出現させるクラス </summary>
public class GetKey : MonoBehaviour
{
    /// <summary> プレイヤーの座標 </summary>
    [Header("プレイヤーの座標(PlayerPrefabが入る)")]
    [SerializeField] private Transform m_player =null;
    /// <summary> ゴールの座標 </summary>
    [Header("ゴールの座標(GoalObjPrefabが入る)")]
    [SerializeField] private Transform m_goalObj = null;
    /// <summary> ゴールオブジェクトの座標を入れる変数 </summary>
    private Transform m_goalObjPos = null;
    /// <summary> GameManagerの座標 </summary>
    [Header("GameManagerの座標(GoalObjPrefabが入る)")]
    [SerializeField] private Transform m_gameManager = null;
    /// <summary> GameManagerの座標を入れる変数 </summary>
    private Transform m_gameManagerPos = null;
    /// <summary> 何度回転させるかの変数 </summary>
    [Header("何度回転させるかの値")]
    [SerializeField] private float m_rotateAngle = default;

    private void Start()
    {
        FindGoal();

        FindGameManager();
    }

    private void Update()
    {
        this.transform.position = new Vector3(0, 0, 0);
        this.transform.Rotate(new Vector3(0, m_rotateAngle * Time.deltaTime, 0));
    }

    /// <summary>
    /// GoalObjを見つけて座標を渡しているメソッド
    /// </summary>
    private void FindGoal()
    {
        //ゴールオブジェクトを見つけて座標を渡してる
        m_goalObj = FindObjectOfType<GoalObj>().transform;
        m_goalObj.GetComponent<GoalObj>().SetKey(transform);

        //ゴールをみつける
        if (m_goalObj != null)
        {
            m_goalObjPos = m_goalObj.transform;
            m_goalObjPos.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("ゴール見つからないよ");
        }
    }

    /// <summary>
    /// GameManagerを見つけて座標を渡しているメソッド
    /// </summary>
    private void FindGameManager()
    {
        //GameManagerを見つけて座標を渡してる
        m_gameManager = FindObjectOfType<GameManager>().transform;
        m_gameManager.GetComponent<GameManager>().SetKey(transform);

        //GameManagerをみつける
        if (m_gameManager != null)
        {
            m_gameManagerPos = m_gameManager.transform;
        }
        else
        {
            Debug.Log("GameManager見つからないよ");
        }
    }

    /// <summary> 
    /// プレイヤーの座標をセットしている 
    /// </summary>
    public void SetPlayer(Transform player)
    {
        m_player = player;
    }

    /// <summary> 
    /// ゴールの座標をセットしている 
    /// </summary>
    public void SetGoal(Transform goal)
    {
        m_goalObj = goal;
    }

    /// <summary>
    /// プレイヤーが鍵に触れたかどうかの当たり判定のメソッド
    /// </summary>
    /// <param name="collider"></param>
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
            m_gameManager.GetComponent<GameManager>().TextStartCoroutine();
            Debug.Log("テキストを表示");
            m_goalObj.GetComponent<GoalObj>().ActiveObj();
        }
    }
}