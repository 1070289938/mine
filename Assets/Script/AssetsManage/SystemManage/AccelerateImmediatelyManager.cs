using System;
using System.Collections;
using System.Collections.Generic;
using TapTap.TapAd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 立即加速
/// </summary>
public class AccelerateImmediatelyManager : MonoBehaviour
{
    Button button;

    public TextMeshProUGUI time; // 标题
    readonly string buttonName = "看广告获取";

    // 广告位 id
    int id = 1041644;
    // 记录按钮上次点击的时间
    public DateTime lastClickTime;
    // 冷却时间，单位：小时
    const int cooldownHours = 1;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(TryWatchAdvertisement);
        // 初始化上次点击时间为一个较早的时间，确保游戏开始时按钮可用

        if (lastClickTime == null)
        {
            lastClickTime = DateTime.MinValue;
        }
    }

    // 每帧更新
    void Update()
    {
        // 计算当前时间与上次点击时间的差值
        TimeSpan elapsedTime = DateTime.Now - lastClickTime;
        if (elapsedTime.TotalHours < cooldownHours)
        {
            // 冷却时间未过，显示剩余冷却时间
            TimeSpan remainingTime = TimeSpan.FromHours(cooldownHours) - elapsedTime;
            time.text = $"{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2} 后刷新";
            // 禁用按钮
            button.interactable = false;
        }
        else
        {
            // 冷却时间已过，显示默认名称
            time.text = buttonName;
            // 启用按钮
            button.interactable = true;
        }
    }

    // 尝试看广告时间加速
    void TryWatchAdvertisement()
    {
        // 计算当前时间与上次点击时间的差值
        TimeSpan elapsedTime = DateTime.Now - lastClickTime;
        if (elapsedTime.TotalHours >= cooldownHours)
        {
            // 冷却时间已过，播放广告
            WatchAdvertisement();
        }
        else
        {
            // 冷却时间未过，可添加提示逻辑，例如显示剩余冷却时间
            Debug.Log($"冷却时间未过，剩余时间: {cooldownHours - elapsedTime.TotalHours:F2} 小时");
        }
    }

    // 看广告获取双倍
    void WatchAdvertisement()
    {
        Example.example.ShowRewardAd(WatchOver, WatchOverOnclick);

    }

    void WatchOverOnclick()
    {
    
        Dictionary<FacilityType, FacilityPanelCount> facility = SaveLoadManager.Instance.GetFacilityPanelCountDictionary();
        Dictionary<ResourceType, double> resource = SaveLoadManager.Instance.CalculatedProduction(facility, 120, 1);//就是2分钟=120秒
        LogManager.Instance.AddLog("成功获得额外加速收益:");
        foreach (var res in resource)
        {
            LogManager.Instance.AddLog(res.Key.GetName() + "+" + AssetsUtil.FormatNumber(res.Value));
            ResourceManager.Instance.AddResource(res.Key, res.Value,false);
        }

    }

    void WatchOver()
    {
        // 更新上次点击时间
        lastClickTime = DateTime.Now;

        Dictionary<FacilityType, FacilityPanelCount> facility = SaveLoadManager.Instance.GetFacilityPanelCountDictionary();
        Dictionary<ResourceType, double> resource = SaveLoadManager.Instance.CalculatedProduction(facility, 600, 1);//就是10分钟=600秒
        LogManager.Instance.AddLog("成功获得时间加速收益:");
        foreach (var res in resource)
        {
            LogManager.Instance.AddLog(res.Key.GetName() + "+" + AssetsUtil.FormatNumber(res.Value));
            ResourceManager.Instance.AddResource(res.Key, res.Value,false);
        }

    }



}