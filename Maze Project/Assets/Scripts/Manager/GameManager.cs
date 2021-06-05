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
    /// <summary> オーディオマネージャーを参照している変数 </summary>
    AudioManager m_audioManager;

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
                break;
            case GameState.InGame:
                break;
            case GameState.Finished:
                Finished();
                break;
        }
    }

    /// <summary> ゴールの関数 </summary>
    public void Finished()
    {
        //ゴール後のUIを非アクティブからアクティブにする
        m_goalCavas.SetActive(true);

        //ゴール後にオーディオ再生
        //m_audioManager.PlaySE(m_audioManager.audioClips[1]);

        // ステータスをゴールした状態に更新する
        m_gameState = GameState.Finished;
    }

    public void OnClickTitle()
    {
        //タイトルに戻るボタン押すとオーディオ再生
        m_audioManager.PlaySE(m_audioManager.audioClips[0]);

        //タイトルにシーンを遷移させる
        SceneManager.LoadScene("Title");
    }
}
