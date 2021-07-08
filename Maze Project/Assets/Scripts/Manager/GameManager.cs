using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> ゲームの進行を管理するクラス </summary>
public class GameManager : MonoBehaviour
{
    /// <summary> ゲームの状態 </summary>
    GameState m_gameState = GameState.NonInitialized;
    /// <summary> ゴール後に表示させるキャンバス </summary>
    [Header("ゴール後に表示させるキャンバス")]
    [SerializeField] private GameObject m_goalCavas = null;
    /// <summary> スタート前のCountdownするテキストの変数 </summary>
    [Header("Countdownするテキスト")]
    [SerializeField] private Text m_countText = null;
    /// <summary> オーディオマネージャーを参照している変数 </summary>
    AudioManager m_audioManager;
    /// <summary> ゲームクリア時に一回だけBGMを鳴らすためのフラグ </summary>
    private bool m_isClear = false;

    /// <summary> ゲームの状態 </summary>
    enum GameState
    {
        /// <summary> 初期化前 </summary>
        NonInitialized,
        /// <summary> 初期化済み、ゲーム開始前 </summary>
        Initialized,
        /// <summary> ゲーム中 </summary>
        InGame,
        /// <summary> ゴール,ゲーム終了 </summary>
        Finished,
        /// <summary> タイムアップ </summary>
        TimeUp,
    }

    private void Start()
    {
        if (GameObject.FindObjectOfType<AudioManager>())
        {
            m_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        }
    }

    void Update()
    {
        switch (m_gameState)
        {
            case GameState.NonInitialized:
                m_gameState = GameState.Initialized;
                break;
            case GameState.Initialized:
                m_gameState = GameState.InGame;
                StartCoroutine(CountStart());
                break;
            case GameState.InGame:
                break;
            case GameState.Finished:
                Finished();
                break;
            case GameState.TimeUp:
                break;
        }
    }

    /// <summary> ゴールの関数 </summary>
    public void Finished()
    {
        //ゴール後のUIを非アクティブからアクティブにする
        m_goalCavas.SetActive(true);

        if (!m_isClear)
        {
            if (m_audioManager)
            {
                //ゴール後にオーディオ再生
                m_audioManager.PlaySE(m_audioManager.audioClips[1]);

                m_isClear = true;
            }  
        }

        // ステートをゴールした状態に更新する
        m_gameState = GameState.Finished;
    }

    public void TimeUP()
    {
        m_gameState = GameState.TimeUp;
    }

    IEnumerator CountStart() 
    {
        m_countText.text = "3";
        yield return new WaitForSeconds(1f);
        m_countText.text = "2";
        yield return new WaitForSeconds(1f);
        m_countText.text = "1";
        yield return new WaitForSeconds(1f);
        m_countText.text = "Start!";
        yield return new WaitForSeconds(0.5f);
        m_countText.gameObject.SetActive(false);
        // ステートをゴールした状態に更新する
        m_gameState = GameState.InGame;
    }

    public void OnClickTitle()
    {
        if (m_audioManager)
        {
            //タイトルに戻るボタン押すとオーディオ再生
            m_audioManager.PlaySE(m_audioManager.audioClips[0]);
        }
        //タイトルにシーンを遷移させる
        SceneManager.LoadScene("Title");
    }
}
