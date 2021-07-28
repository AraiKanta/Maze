using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> ゲームの進行を管理するクラス </summary>
public class GameManager : MonoBehaviour
{
    /// <summary> ゴール後に表示させるキャンバス </summary>
    [Header("ゴール後に表示させるキャンバス")]
    [SerializeField] private GameObject m_goalCavas = null;
    /// <summary> ゲームオーバー後に表示させるキャンバス </summary>
    [Header("ゲームオーバー後に表示させるキャンバ")]
    [SerializeField] private GameObject m_timeUpCavas = null;
    /// <summary> オーディオマネージャーを参照している変数 </summary>
    AudioManager m_audioManager;
    /// <summary> ゲームクリア時に一回だけBGMを鳴らすためのフラグ </summary>
    private bool m_isClear = false;
    /// <summary> 鍵オブジェクトの変数 </summary>
    [Header("鍵の座標(KeyObjPrefabが入る)")]
    [SerializeField] private Transform m_keyObj = null;
    /// <summary> 鍵を獲得した時に表示するテキスト </summary>
    [Header("鍵を獲得した時に表示するテキスト")]
    [SerializeField] Text m_getKeyText = null;
    ///<summary> ゲームの状態 </summary>
    [Header("現在のゲームの状態")]
    public GameState m_gameState;

    /// <summary> 
    /// ゲームの状態 
    /// </summary>
    public enum GameState
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
        m_getKeyText.gameObject.SetActive(false);
        Debug.Log("テキスト非表示");

        m_gameState = GameState.NonInitialized;

        if (FindObjectOfType<AudioManager>())
        {
            m_audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        }
    }

    private void Update()
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
                Debug.Log("InGame");
                break;
            case GameState.Finished:
                Finished();
                break;
            case GameState.TimeUp:
                TimeUP();
                break;
        }
    }

    /// <summary> 
    /// ゴールの関数 
    /// </summary>
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

        Debug.Log("GameClear");

        // ステートをゴールした状態に更新する
        m_gameState = GameState.Finished;
    }

    /// <summary>
    /// 制限時間によるゲームオーバー時のメソッド
    /// </summary>
    public void TimeUP()
    {
        //ゲームオーバー後のUIを非アクティブからアクティブにする
        m_timeUpCavas.SetActive(true);

        Debug.Log("TimeUp");

        // ステートをTimeUPした状態に更新する
        m_gameState = GameState.TimeUp;
    } 

    /// <summary>
    /// ClickしたときにTitleに戻る処理を行っているメソッド
    /// </summary>
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

    /// <summary> 
    /// 鍵の座標をセットしている 
    /// </summary>
    public void SetKey(Transform key)
    {
        m_keyObj = key;
    }

    /// <summary> 
    /// 鍵を獲得したときにテキストを表示するメソッド 
    /// </summary>
    IEnumerator GetKeyText()
    {
        m_getKeyText.GetComponent<Text>();
        m_getKeyText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        m_getKeyText.gameObject.SetActive(false);
    }
    public void TextStartCoroutine()
    {
        StartCoroutine(GetKeyText());
    }
}
