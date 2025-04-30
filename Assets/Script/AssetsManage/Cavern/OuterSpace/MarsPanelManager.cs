using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 殖民面板
/// </summary>
public class MarsPanelManager : MonoBehaviour
{
    public static MarsPanelManager Instance;

    public VerticalLayoutGroup verticalLayoutGroup;


    public MarsColonyManager marsColonyManager;//殖民地

    int maxCount = 0;//最大殖民点数

    int thisCount = 0;//当前殖民点数

    public TextMeshProUGUI count;

    public int GetMaxCount()
    {
        return maxCount;
    }

    public int GetThisCount()
    {
        return thisCount;
    }

    public void LoadSave(int count)
    {
        thisCount = count;
    }



    public void Activate()
    {
        gameObject.SetActive(true);
        verticalLayoutGroup.padding.top = 120;

    }

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }







    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 更新殖民点数上限
    /// </summary>
    void UpdateMaxMars()
    {
        maxCount = marsColonyManager.GetUp();

        count.text = thisCount + " / " + maxCount;

    }



    void Update()
    {
        UpdateMaxMars();
    }

    /// <summary>
    /// 获取剩余点数
    /// </summary>
    /// <returns></returns>
    public int GetRemainingDemand()
    {


        return maxCount - thisCount;
    }
    /// <summary>
    /// 添加当前点数
    /// </summary>
    /// <param name="count"></param>
    public void AddThisCount(int count)
    {
        thisCount += count;
    }


}