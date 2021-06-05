using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary> アプリケーションを終了させるクラス </summary>
public class AppEndManager : MonoBehaviour
{
    /// <summary>
    /// 終了ボタン
    /// </summary>
    [System.Obsolete]
    private void OnApplicationQuit()
    {
        // 終了処理をキャンセル
        Application.CancelQuit();
    }

    /// <summary>
    /// 終了
    /// </summary>
    public void OnExit()
    {
        Application.Quit();
    }
}
