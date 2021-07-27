using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> 難易度設定用のクラス </summary>
public class DifficultyManager : MonoBehaviour
{
    /// <summary> マップサイズを格納する変数 </summary>
    private int m_size = default(int); 

    public void Difficulty(Scene next, LoadSceneMode mode) 
    {
        GameObject.Find("MazeGanerator").GetComponent<MazeGanerator>().setMazeSize = m_size;

        SceneManager.sceneLoaded -= Difficulty;
    }

    /// <summary>
    /// ボタンが押されたときにマップサイズを変更するメソッドEasy() 
    /// </summary>
    public void Easy() 
    {
        m_size = 11;
    }
    /// <summary>
    /// ボタンが押されたときにマップサイズを変更するメソッドNomal()
    /// </summary>
    public void Nomal()
    {
        m_size = 21;
    }
    /// <summary>
    /// ボタンが押されたときにマップサイズを変更するメソッドHeard()
    /// </summary>
    public void Heard()
    {
        m_size = 31;
    }
}
