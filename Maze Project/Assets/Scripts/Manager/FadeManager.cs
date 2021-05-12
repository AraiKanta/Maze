using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeManager : MonoBehaviour
{
    /// <summary> 値保管用 </summary>
    float alp;
    /// <summary> シーン遷移を開始するまでの時間 </summary>
    [Header("シーン遷移を開始するまでの時間")]
    public float fadeStartTime;
    /// <summary> 現在のステージ番号 </summary>
    [Header("現在のステージ番号")]
    public int currentStageNum = 0;
    /// <summary> ステージ名 </summary>
    [Header("ステージ名")]
    public string[] stageName;
    /// <summary> Panelオブジェクト </summary>
    [Header("Panelオブジェクト")]
    public GameObject panel;

    void Start()
    {
        alp = panel.GetComponent<Image>().color.a;

        currentStageNum += 1;
    }

    //フェードアウトの処理
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

    public void OnClick()
    {
        StartCoroutine(FadePanel());
    }
}
