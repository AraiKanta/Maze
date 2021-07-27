using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> Audioを管理するクラス </summary>
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    /// <summary>タイトル画面のBGM</summary>
    public AudioClip titleBGM;
    /// <summary>各ステージのBGM</summary>
    public AudioClip gameBGM;
    /// <summary> 複数のAudioClipを扱う。index番号を設定 </summary>
    [Header("複数のAudioClipを扱う。index番号を設定")]
    public AudioClip[] audioClips;
    /// <summary>trueでmute</summary>
    public bool m_mute = false;
    /// <summary>BGMのフェードアウトスピード</summary>
    [SerializeField] float fadeSpeed;

    [SerializeField] public AudioSource audioSourceBGM;
    [SerializeField] public AudioSource audioSourceSE;
    void Start()
    {
        if (GameObject.FindObjectOfType<SaveManager>())
        {
            VolumeChangeBGM(SaveManager.saveData.m_BGMVolume);
            VolumeChangeSE(SaveManager.saveData.m_SEVolume);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (SceneManager.GetActiveScene().name == "Title")
        {
            PlayBgm(titleBGM);
        }
    }
    // シーンが切り替わったときにシーン名ごとにBGMを切り替える
    void OnSceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        switch (SceneManager.GetActiveScene().name)
        { 
            case "Title":
                PlayBgm(titleBGM);
                audioSourceBGM.loop = true;
                break;
            case "Credit":
                PlayBgm(titleBGM);
                audioSourceBGM.loop = true;
                break;
            case "EasyScene":
                PlayBgm(gameBGM);
                audioSourceBGM.loop = true;
                break;
            case "NormalScene":
                PlayBgm(gameBGM);
                audioSourceBGM.loop = true;
                break;
            case "HardScene":
                PlayBgm(gameBGM);
                audioSourceBGM.loop = true;
                break;
        }
    }

    //BGMを再生
    public void PlayBgm(AudioClip bgm)
    {
        audioSourceBGM.clip = bgm;
        audioSourceBGM.loop = false;
        audioSourceBGM.Play();
    }

    //ミュートを切り替える
    public void Mute()
    {
        audioSourceSE.mute = m_mute;
        audioSourceBGM.mute = m_mute;
    }

    //現在再生中のBGMをフェードアウト
    public IEnumerator FadeOutBGM()
    {
        while (audioSourceBGM.volume > 0)
        {
            audioSourceBGM.volume -= fadeSpeed;
            yield return new WaitForSeconds(0.017f);
        }
    }  
    //SEを再生
    public void PlaySE(AudioClip SE)
    {
        audioSourceSE.pitch = 1;
        audioSourceSE.loop = false;
        audioSourceSE.PlayOneShot(SE);
    }

    /// <summary>
    /// SEをループ再生
    /// </summary>
    /// <param name="SE"></param>
    public void PlaySELoop(AudioClip SE)
    {
        audioSourceSE.pitch = 1;
        audioSourceSE.loop = true;
        audioSourceSE.PlayOneShot(SE);
    }

    /// <summary>BGMの音量変更を反映</summary>
    public void VolumeChangeBGM(float volume)
    {
        audioSourceBGM.volume = volume;
    }

    /// <summary>SEの音量変更を反映</summary>
    public void VolumeChangeSE(float volume)
    {
        audioSourceSE.volume = volume;
    }
}
