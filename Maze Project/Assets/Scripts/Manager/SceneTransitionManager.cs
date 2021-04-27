using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    /// <summary> フェードさせるまでの時間 </summary>
    [Header("フェードさせるまでの時間")]
    [SerializeField] readonly float m_fadeTime = 0.4f;
    /// <summary> フェードしている時間 </summary>
    [Header("フェードしている時間")]
    [SerializeField] float m_fadeDletaTime = 0;
    /// <summary> フェードさせるImage </summary>
    [Header("フェードさせるImage")]
    [SerializeField] Image m_fadeImage = null; 
    
    /// <summary>
    /// フェードインのコルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator FadeInCoroutine()
    {
        float m_alpha = 1;
        Color color = new Color(0, 0, 0, m_alpha);
        this.m_fadeDletaTime = 0;
        this.m_fadeImage.color = color;
        do
        {
            yield return null;
            this.m_fadeDletaTime += Time.unscaledDeltaTime;
            m_alpha = 1 - (this.m_fadeDletaTime / this.m_fadeTime);
            if (m_alpha < 0)
            {
                m_alpha = 0;
            }
            color.a = m_alpha;
            this.m_fadeImage.color = color;
        }
        while (this.m_fadeDletaTime <= this.m_fadeTime);
    }

    /// <summary>
    /// フェードアウトのコルーチン
    /// </summary>
    /// <param name="nextScene"></param>
    /// <returns></returns>
    private IEnumerator FadeOutCoroutine(string nextScene)
    {
        float m_alpha = 0;
        Color color = new Color(0, 0, 0, m_alpha);
        this.m_fadeDletaTime = 0;
        this.m_fadeImage.color = color;
        do
        {
            yield return null;
            this.m_fadeDletaTime += Time.unscaledDeltaTime;
            m_alpha = this.m_fadeDletaTime / this.m_fadeTime;
            if (m_alpha > 1)
            {
                m_alpha = 1;
            }
            color.a = m_alpha;
            this.m_fadeImage.color = color;
        }
        while (this.m_fadeDletaTime <= this.m_fadeTime);

        SceneManager.LoadScene(nextScene);
    }

    public void FadeIn()
    {
        IEnumerator coroutine = FadeInCoroutine();
        StartCoroutine(coroutine);
    }
    public void FadeOut(string nextScene)
    {
        IEnumerator coroutine = FadeOutCoroutine(nextScene);
        StartCoroutine(coroutine);
    }
}
