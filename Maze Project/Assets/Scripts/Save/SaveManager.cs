using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[System.Serializable]
public struct SaveData
{
    /// <summary>SEのボリューム</summary>
    public float m_SEVolume;
    /// <summary>BGMのボリューム</summary>
    public float m_BGMVolume;
}

/// <summary> セーブデータの管理するクラス </summary>
public class SaveManager : SingletonMonoBehaviour<SaveManager>
{
    /// <summary>セーブデータ
    /// ここから読み込み書き込みを行う
    /// </summary>
    public static SaveData saveData;
    /// <summary>セーブデータの保存先</summary>
    public string SaveDataPath { get; private set; }

    /// <summary>
    /// deckImagePathがなければ作成
    /// 保存されているDeckDataを読み込み、なければ初期化
    /// </summary>
    private void Awake()
    {
        SaveDataPath = Application.dataPath + @"\savedata.json";
        if (File.Exists(SaveDataPath))
        {
            saveData = LoadData(SaveDataPath);
            Debug.Log("セーブデータを読み込みます。");
        }
        else
        {
            CannotLoad();
            Debug.Log("セーブデータが存在しません。");
        }
    }

    /// <summary>jsonデータを読み込み</summary>
    /// <param name="jsonPath"></param>
    /// <returns></returns>
    private SaveData LoadData(string jsonPath)
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(jsonPath);
        datastr = reader.ReadToEnd();
        reader.Close();
        return JsonUtility.FromJson<SaveData>(datastr);
    }

    /// <summary>読み込みできなかった時の初期化</summary>
    private void CannotLoad()
    {
        saveData = new SaveData();
        saveData.m_SEVolume = 0.5f;
        saveData.m_BGMVolume = 0.5f;
    }

    /// <summary>アプリを閉じたとき自動で保存</summary>
    private void OnApplicationQuit()
    {
        Save(saveData);
    }

    /// <summary>セーブデータをもらってそれを保存</summary>
    /// <param name="beSavedData">保存したいデータ</param>
    private void Save(SaveData beSavedData)
    {
        StreamWriter writer;
        string jsonstr = JsonUtility.ToJson(beSavedData);

        writer = new StreamWriter(Application.dataPath + @"\savedata.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
}
