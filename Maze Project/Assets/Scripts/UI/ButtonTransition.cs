using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> ボタンでシーン遷移させるだけのスクリプト </summary>
public class ButtonTransition : MonoBehaviour
{
    /// <summary> シーン名を指定 </summary>
    [Header("シーン名を指定")]
    [SerializeField] private string m_sceneName = null;

   public void OnClick()
   {
        SceneManager.LoadScene(m_sceneName);
   }
}
