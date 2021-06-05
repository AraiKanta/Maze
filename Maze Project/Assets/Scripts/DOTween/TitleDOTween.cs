using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary> タイトルのスプライトをDOTweenで点滅させるためのクラス </summary>
public class TitleDOTween : MonoBehaviour
{
    /// <summary> 遅延させる時間 </summary>
    [Header("点滅の間隔の時間")]
    [SerializeField] private float m_duration;
    /// <summary> 難易度選択Imageの変数 </summary>
    [Header("難易度選択のImage")] 
    [SerializeField] private Image m_touchToStart = null;

    void Start()
    {
        m_touchToStart = GetComponent<Image>();

        var sequence = DOTween.Sequence();
        sequence.Append(m_touchToStart.DOFade(0.0f, this.m_duration).SetEase(Ease.InOutFlash))
                .SetLoops(-1, LoopType.Yoyo)
                .Play();
    }
}
