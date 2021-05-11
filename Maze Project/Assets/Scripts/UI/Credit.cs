using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Creditを表示するクラス </summary>
public class Credit : MonoBehaviour
{
    [Header("テキストのスクロールスピード")]
    [SerializeField] private float textScrollSpeed = 30;
    [Header("テキストの制限位置")]
    [SerializeField] private float limitPosition = 730f;
    /// <summary>エンドロールが終了したかどうか </summary>
    private bool isStopStaffRoll;
    /// <summary>シーン移動用コルーチン </summary>
    private Coroutine _staffRollCoroutine;

    void Update()
    {
        // エンドロールが終了した時
        if (isStopStaffRoll)
        {
            _staffRollCoroutine = StartCoroutine(GoToNextScene());
        }
        else
        {
            // エンドロール用テキストがリミットを越えるまで動かす
            if (transform.position.y <= limitPosition)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y + textScrollSpeed * Time.deltaTime);
            }
            else
            {
                isStopStaffRoll = true;
            }
        }
    }

    IEnumerator GoToNextScene()
    {
        // 5秒間待つ
        yield return new WaitForSeconds(2f);

        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine(_staffRollCoroutine);
        }

        yield return null;
    }
}
