using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> オプションを管理するクラス </summary>
public class OptionManager : MonoBehaviour
{
    /// <summary>オーディオマネージャーを参照している変数</summary>
    AudioManager m_audioManager;
    /// <summary> BGM調整用のスライダーの変数 </summary>
    [Header("BGM調整用のスライダー")]
    [SerializeField] private Slider m_sliderBGM;
    /// <summary> SE調整用のスライダーの変数 </summary>
    [Header("SE調整用のスライダー")]
    [SerializeField] private Slider m_sliderSE;
    /// <summary> オプションのキャンバスの変数 </summary>
    [Header("オプションのキャンバス")]
    [SerializeField] private Canvas m_optionCanvas;


    void Start()
    {
        if (GameObject.FindObjectOfType<SaveManager>())
        {
            m_sliderBGM.value = SaveManager.saveData.m_BGMVolume;
            m_sliderSE.value = SaveManager.saveData.m_SEVolume;
        }
        if (GameObject.FindObjectOfType<AudioManager>())
        {
            m_audioManager = GameObject.FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
        }
    }

    /// <summary>
    /// BGMとSEのボリューム変更をsaveDataにする。
    /// </summary>
    /// <param name="isBGM"></param>
    public void VolumeValueChanged(bool isBGM)
    {
        if (isBGM)
        {
            if (m_audioManager)
            {
                m_audioManager.VolumeChangeBGM(m_sliderBGM.value);
            }

            SaveManager.saveData.m_BGMVolume = m_sliderBGM.value;

        }
        else
        {
            if (m_audioManager)
            {
                m_audioManager.VolumeChangeSE(m_sliderSE.value);
            }

            SaveManager.saveData.m_SEVolume = m_sliderSE.value;

        }
    }

    /// <summary>
    /// オプションキャンバスを表示する
    /// </summary>
    public void OnCanvas()
    {
        if (m_audioManager)
        {
            m_audioManager.PlaySE(m_audioManager.audioClips[0]);
        }
        m_optionCanvas.gameObject.SetActive(true);
    }

    /// <summary>
    /// タイトルに戻るときのSE
    /// </summary>
    public void Return()
    {
        if (m_audioManager)
        {
            m_audioManager.PlaySE(m_audioManager.audioClips[0]);
        }
        this.gameObject.SetActive(false);
    }
}
