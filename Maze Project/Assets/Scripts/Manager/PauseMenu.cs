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
    /// <summary> Pause用のリトライボタンの変数 </summary>
    [Header("Pause用のリトライボタンボタン")]
    [SerializeField] private Button m_retryButton = null;
    /// <summary> Textの変数 </summary>
    [Header("Pause用のテキスト")]
    [SerializeField] private GameObject m_text = null;
    /// <summary> 遷移させるシーンの名前の変数 </summary>
    [Header("遷移させるシーン名")]
    [SerializeField] private string m_sceneName = null;
   

    private void Start()
    {
        m_puaseButton.onClick.AddListener(Pause);
        m_retryButton.gameObject.SetActive(!m_retryButton.gameObject.activeSelf);
        m_text.SetActive(!m_text.activeSelf);
    }

    public void Pause()
    {
        m_retryButton.gameObject.SetActive(m_retryButton.gameObject.activeSelf);
        m_text.SetActive(m_text.activeSelf);

        if (m_retryButton.gameObject.activeSelf && m_text.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        m_puaseButton.onClick.AddListener(Pause);
        m_puaseButton.onClick.RemoveListener(Release);
    }

    public void Release()
    {
        
       
        m_puaseButton.onClick.AddListener(Release);
        m_puaseButton.onClick.RemoveListener(Pause);
    }

    public void Retry()
    {
        if (m_retryButton.gameObject.activeSelf && m_text.activeSelf)
        {
            Time.timeScale = 1f;

            m_retryButton.gameObject.SetActive(!m_retryButton.gameObject.activeSelf);
            m_text.SetActive(!m_text.activeSelf);
        }
        SceneManager.LoadScene(m_sceneName);
    }
}
