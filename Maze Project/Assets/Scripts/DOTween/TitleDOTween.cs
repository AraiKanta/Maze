using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleDOTween : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] Image _touchToStart = null;

    void Start()
    {
        _touchToStart = GetComponent<Image>();

        var sequence = DOTween.Sequence();
        sequence.Append(_touchToStart.DOFade(0.0f, this.duration).SetEase(Ease.InOutFlash))
                .SetLoops(-1, LoopType.Yoyo)
                .Play();
    }
}
