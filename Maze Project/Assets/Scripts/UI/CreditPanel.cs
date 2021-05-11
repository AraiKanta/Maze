using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Creditを表示させるパネルを制御するクラス </summary>
public class CreditPanel : MonoBehaviour
{
    /// <summary> Creditを表示させるボタンの変数 </summary>
    [SerializeField] Button m_creditButton = null;
    /// <summary> Creditを表示するパネルの変数 </summary>
    [SerializeField] GameObject m_panel = null;
    private void Start()
    {
        m_creditButton.onClick.AddListener(OnClick);
    }

    public void OnClick() 
    {
        //Credit用のパネルをアクティブにする
        m_panel.SetActive(true);

        m_creditButton.onClick.RemoveListener(OnClick);
        m_creditButton.onClick.AddListener(ReverseOnClick);
    }

    public void ReverseOnClick()
    {
        //Credit用のパネルを非アクティブにする
        m_panel.SetActive(false);

        m_creditButton.onClick.RemoveListener(ReverseOnClick);
        m_creditButton.onClick.AddListener(OnClick);
    }
}
