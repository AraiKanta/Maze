using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>制限時間を制御するクラス</summary>
public class TimeManager : MonoBehaviour
{
    /// <summary> スタート前のCountdownするテキストの変数 </summary>
    [Header("スタートCountdownするテキスト")]
    [SerializeField] private Text m_countText = null;
    /// <summary> トータル制限時間 </summary>
    [Header("トータル制限時間")]
    private float m_totalTime = default(float);
    /// <summary> 制限時間（分） </summary>
    [Header("制限時間（分）")]
    [SerializeField] private int m_minute = default(int);
    /// <summary> 制限時間（秒） </summary>
    [Header("制限時間（秒）")]
    [SerializeField] private float m_seconds = default(float);
    /// <summary> 残り何秒でTimerTextが赤くなるか </summary>
    [Header("残り何秒でTimerTextが赤くなるか")]
    [SerializeField] float m_startWarning = default(float);
    /// <summary> CountDownTimerのテキストの変数 </summary>
    [Header("CountDownTimerのテキスト")]
    [SerializeField] private Text m_timerText = null;
    /// <summary> 遅延させる時間 </summary>
    [Header("点滅の間隔の時間")]
    [SerializeField] private float m_duration = default(float);
    /// <summary> 古い時間を格納しておく変数 </summary>
    private float m_oldSeconds = default(float);
    /// <summary> 参照しているクラスの変数 </summary>
    GameManager m_gameManger = null;

    void Start()
    {
        m_totalTime = m_minute * 60 + m_seconds;
        m_oldSeconds = 0f;
        m_timerText = GetComponentInChildren<Text>();

        StartCoroutine(CountStart());
    }

    /// <summary>
    /// カウントダウンを行うコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator CountStart()
    {
        m_countText.text = "3";
        yield return new WaitForSeconds(1f);
        m_countText.text = "2";
        yield return new WaitForSeconds(1f);
        m_countText.text = "1";
        yield return new WaitForSeconds(1f);
        m_countText.text = "鍵を獲得しろ！";
        yield return new WaitForSeconds(2f);
        m_countText.gameObject.SetActive(false);

        StartCoroutine(CountDown());
    }

    /// <summary>
    /// 制限時間の処理を行うメソッド
    /// </summary>
    IEnumerator CountDown()
    {
        while (true)
        {
            //　一旦トータルの制限時間を計測；
            m_totalTime = m_minute * 60 + m_seconds;
            m_totalTime -= Time.deltaTime;

            //　再設定
            m_minute = (int)m_totalTime / 60;
            m_seconds = m_totalTime - m_minute * 60;

            if (m_totalTime < m_startWarning)
            {
                WarnigTime();
            }

            //　タイマー表示用UIテキストに時間を表示する
            if ((int)m_seconds != (int)m_oldSeconds)
            {
                m_timerText.text = m_minute.ToString("00") + ":" + ((int)m_seconds).ToString("00");
            }
            m_oldSeconds = m_seconds;

            //　制限時間以下になったらコンソールに『TIME UP』という文字列を表示する
            if (m_totalTime <= 0f)
            {
                Debug.Log("TIME UP");

                //GameManagerに渡してる
                m_gameManger = GameObject.FindObjectOfType<GameManager>();
                if (m_gameManger)
                {
                    m_gameManger.TimeUP();
                }

                break;
            }

            yield return null;
        } 
    }

    /// <summary> 
    /// 制限時間が少なくなると赤く点滅をさせるメソッド 
    /// </summary>
    private void WarnigTime()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(m_timerText.DOColor(Color.red, m_duration))
                .Append(m_timerText.DOFade(0.0f, m_duration).SetEase(Ease.InOutFlash))
                .SetLoops(-1, LoopType.Yoyo)
                .Play();
    }
}