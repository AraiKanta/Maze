using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeManager : MonoBehaviour
{
    /// <summary> 値保管用 </summary>
    private float alp;
    /// <summary> シーン遷移を開始するまでの時間 </summary>
    [Header("シーン遷移を開始するまでの時間")]
    [SerializeField] private float fadeStartTime;
    /// <summary> 現在のステージ番号 </summary>
    [Header("現在のステージ番号")]
    [SerializeField] private int currentStageNum = 0;
    /// <summary> ステージ名 </summary>
    [Header("ステージ名")]
    [SerializeField] private string[] stageName;
    /// <summary> Panelオブジェクト </summary>
    [Header("Panelオブジェクト")]
    [SerializeField] private GameObject panel;
    /// <summary> オーディオソースの変数 </summary>
    [Header("オーディオソース")]
    [SerializeField] private AudioSource m_audioSource = null;
    /// <summary> オーディオクリップの変数 </summary>
    [Header("オーディオクリップ")]
    [SerializeField] private AudioClip m_audioClip = null; 

    void Start()
    {
        m_audioSource = GetComponentInChildren<AudioSource>();

        alp = panel.GetComponent<Image>().color.a;

        currentStageNum += 1;
    }

    /// <summary> フェードアウトの処理 </summary>
    IEnumerator FadePanel()
    {
        while (alp < 1)
        {
            fadeStartTime += Time.deltaTime;
            panel.GetComponent<Image>().color += new Color(0, 0, 0, 0.1f * fadeStartTime);
            alp += 0.01f;
            yield return new WaitForEndOfFrame();
        }
        SceneManager.LoadSceneAsync(stageName[currentStageNum]);

        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
        }
    }

    /// <summary> Buttonを押したときの処理 </summary>
    public void OnClick()
    {
        m_audioSource.PlayOneShot(m_audioClip);

        StartCoroutine(FadePanel());
    }
}
