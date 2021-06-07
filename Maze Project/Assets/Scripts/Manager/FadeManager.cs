using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> シーン遷移時のフェードを管理するクラス </summary>
public class FadeManager : MonoBehaviour
{
    /// <summary> 値保管用 </summary>
    private float alp;
    /// <summary> シーン遷移を開始するまでの時間 </summary>
    [Header("シーン遷移を開始するまでの時間")]
    [SerializeField] private float m_fadeStartTime;
    /// <summary> 現在のステージ番号 </summary>
    [Header("現在のステージ番号")]
    [SerializeField] private int m_currentStageNum = 0;
    /// <summary> ステージ名 </summary>
    [Header("ステージ名")]
    [SerializeField] private string[] m_stageName;
    /// <summary> Panelオブジェクト </summary>
    [Header("Panelオブジェクト")]
    [SerializeField] private GameObject m_panel = null;
    /// <summary> オーディオマネージャーを参照している変数 </summary>
    AudioManager m_audioManager;

    void Start()
    {
        if (GameObject.FindObjectOfType<AudioManager>())
        {
            m_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        }

        alp = m_panel.GetComponent<Image>().color.a;

        m_currentStageNum += 1;
    }

    /// <summary> フェードアウトの処理 </summary>
    IEnumerator FadePanel()
    {
        while (alp < 1)
        {
            m_fadeStartTime += Time.deltaTime;
            m_panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.1f * m_fadeStartTime);
            alp += 0.01f;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadSceneAsync(m_stageName[m_currentStageNum]);

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    /// <summary> Buttonを押したときの処理 </summary>
    public void OnClick()
    {
        if (m_audioManager)
        {
            //音ならす
            m_audioManager.PlaySE(m_audioManager.audioClips[0]);
        }

        StartCoroutine(FadePanel());
    }
}
