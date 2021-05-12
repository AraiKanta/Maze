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
    [SerializeField] GameObject m_goalCavas = null;

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

    void Update()
    {
        switch (m_gameState)
        {
            case GameState.NonInitialized:
                //Debug.Log("現在の状態 : NonInitialized");
                m_gameState = GameState.Initialized;
                break;
            case GameState.Initialized:
                //Debug.Log("現在の状態 : Initialized");
                m_gameState = GameState.InGame;
                break;
            case GameState.InGame:
                //Debug.Log("現在の状態 : InGame");
                break;
            case GameState.Finished:
                //Debug.Log("現在の状態 : Finished");
                Finished();
                break;
        }
    }

    /// <summary> ゴールの関数 </summary>
    public void Finished()
    {
        //ゴール後のUIを非アクティブからアクティブにする
        m_goalCavas.SetActive(true);

        // ステータスをゴールした状態に更新する
        m_gameState = GameState.Finished;
    }

    public void OnClickTitle()
    {
        //タイトルにシーンを遷移させる
        SceneManager.LoadScene("Title");
    }
}
