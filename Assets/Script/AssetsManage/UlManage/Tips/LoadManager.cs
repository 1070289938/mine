using System.Collections;
using System.Collections.Generic;
using TapTap.TapAd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 导入存档
/// </summary>
public class LoadManager : MonoBehaviour
{
    /// <summary>
    /// 代码内容
    /// </summary>
    public TMP_InputField content;



    //取消按钮
    public Button drawDown;

    //导入按钮
    public Button doubleClaim;

    //粘贴按钮
    public Button affix;


    // Start is called before the first frame update
    void Start()
    {
        drawDown.onClick.AddListener(ReceiveIncome);
        doubleClaim.onClick.AddListener(WatchAdvertisement);
        affix.onClick.AddListener(Affix);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 取消
    /// </summary>
    public void ReceiveIncome()
    {
        TipsManager.Instance.OnClickAll();
    }



    // 导入
    void WatchAdvertisement()
    {
        SaveLoadManager.Instance.ImportSaveData(content.text);
        TipsManager.Instance.OnClickAll();
    }

    void Affix()
    {
        string clipboardText = GUIUtility.systemCopyBuffer;
        if (string.IsNullOrEmpty(clipboardText))
        {
            LogManager.Instance.AddLog("粘贴板没有内容");
            // 这里可以添加处理剪贴板内容的逻辑，例如导入存档等
        }
        content.text = clipboardText;
    }

}
