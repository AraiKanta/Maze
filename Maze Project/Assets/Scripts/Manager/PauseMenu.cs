using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> Pauseメニュの出すクラス </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary> Pause用のボタンの変数 </summary>
    [Header("Pause用のボタン")]
    [SerializeField] private Button m_puaseButton = null;
    /// <summary> Pause用の戻るボタンの変数 </summary>
    [Header("Pause用の戻るボタン")]
    [SerializeField] private GameObject m_releaseButton = null;
    /// <summary> Pause用のリトライボタンの変数 </summary>
    [Header("Pause用のリトライボタンボタン")]
    [SerializeField] private GameObject m_retryButton = null;
    /// <summary> Textの変数 </summary>
    [Header("Pause用のテキスト")]
    [SerializeField] private GameObject m_textPanel = null;
    /// <summary> 遷移させるシーンの名前の変数 </summary>
    [Header("遷移させるシーン名")]
    [SerializeField] private string m_sceneName = null;
    /// <summary> オーディオソースの変数 </summary>
    [Header("オーディオソース")]
    [SerializeField] private AudioSource m_audioSource = null;
    /// <summary> オーディオクリップの変数 </summary>
    [Header("オーディオクリップ")]
    [SerializeField] private AudioClip m_audioClip = null;

    private void Start()
    {
        m_audioSource = GetComponentInChildren<AudioSource>();

        //オブジェクトを非アクティブにしておく
        m_releaseButton.SetActive(false);
        m_retryButton.SetActive(false);
        m_textPanel.SetActive(false);
    }

    /// <summary>
    /// ポーズ
    /// </summary>
    public void Pause()
    {
        //タイムスケールを0にして止める
        Time.timeScale = 0f;

        //オブジェクトをアクティブにする
        m_releaseButton.SetActive(true);
        m_retryButton.SetActive(true);
        m_textPanel.SetActive(true);

        //音ならす
        m_audioSource.PlayOneShot(m_audioClip);
    }

    /// <summary>
    /// ゲームに戻る
    /// </summary>
    public void Release()
    {
        //タイムスケールを1にして止める
        Time.timeScale = 1f;

        //オブジェクトを非アクティブにする
        m_releaseButton.SetActive(false);
        m_retryButton.SetActive(false);
        m_textPanel.SetActive(false);

        //音ならす
        m_audioSource.PlayOneShot(m_audioClip);
    }

    /// <summary>
    /// ゲームをやり直す。自動マップせ制のため違うステージになる。
    /// </summary>
    public void Retry()
    {
        //タイムスケールを1にして止める
        Time.timeScale = 1f;

        //オブジェクトを非アクティブにする
        m_releaseButton.SetActive(false);
        m_retryButton.SetActive(false);
        m_textPanel.SetActive(false);

        //音ならす
        m_audioSource.PlayOneShot(m_audioClip);

        //指定したシーンに遷移
        SceneManager.LoadScene(m_sceneName);
    }
}
