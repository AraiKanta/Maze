using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> アプリケーションを終了させるクラス </summary>
public class AppEndManager : MonoBehaviour
{
    /// <summary> オーディオマネージャーを参照している変数 </summary>
    AudioManager m_audioManager;

    private void Start()
    {
        if (GameObject.FindObjectOfType<AudioManager>())
        {
            m_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        }
    }

    /// <summary>
    /// 終了ボタン
    /// </summary>
    [System.Obsolete]
    private void OnApplicationQuit()
    {
        // 終了処理をキャンセル
        Application.CancelQuit();
    }

    /// <summary>
    /// 終了
    /// </summary>
    public void OnExit()
    {
        Application.Quit();

        if (m_audioManager)
        {
            //音ならす
            m_audioManager.PlaySE(m_audioManager.audioClips[0]);
        }
    }
}
