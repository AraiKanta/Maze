using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyManager : MonoBehaviour
{

    private int size = default; 


    public void Difficulty(Scene next, LoadSceneMode mode) 
    {
        GameObject.Find("MazeGanerator").GetComponent<MazeGanerator>().setMazeSize = size;

        SceneManager.sceneLoaded -= Difficulty;
    }

    /// <summary>
    /// ボタンが押されたときにマップサイズを変更するメソッドEasy() 
    /// </summary>
    public void Easy() 
    {
        size = 7;
    }
    /// <summary>
    /// ボタンが押されたときにマップサイズを変更するメソッドNomal()
    /// </summary>
    public void Nomal()
    {
        size = 15;
    }
    /// <summary>
    /// ボタンが押されたときにマップサイズを変更するメソッドHeard()
    /// </summary>
    public void Heard()
    {
        size = 21;
    }
}
