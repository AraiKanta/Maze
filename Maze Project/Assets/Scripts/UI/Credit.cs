using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Creditを表示するクラス </summary>
public class Credit : MonoBehaviour
{
    /// <summary> テキストのスクロールスピード </summary>
    [Header("テキストのスクロールスピード")]
    [SerializeField] private float m_textScrollSpeed = 30;
    /// <summary> テキストの制限位置 </summary>
    [Header("テキストの制限位置")]
    [SerializeField] private float m_limitPosition = 730f;
    /// <summary>エンドロールが終了したかどうか </summary>
    private bool isStopStaffRoll;
    /// <summary>シーン移動用コルーチン </summary>
    private Coroutine m_staffRollCoroutine;

    void Update()
    {
        // エンドロールが終了した時
        if (isStopStaffRoll)
        {
            m_staffRollCoroutine = StartCoroutine(GoToNextScene());
        }
        else
        {
            // エンドロール用テキストがリミットを越えるまで動かす
            if (transform.position.y <= m_limitPosition)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + m_textScrollSpeed * Time.deltaTime);
            }
            else
            {
                isStopStaffRoll = true;
            }
        }
    }

    /// <summary>
    /// スクロールが終わったとき
    /// </summary>
    /// <returns></returns>
    IEnumerator GoToNextScene()
    {
        // 2秒間待つ
        yield return new WaitForSeconds(2f);

        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(m_staffRollCoroutine);
            SceneManager.LoadScene("Title");
        }

        yield return null;
    }
}
