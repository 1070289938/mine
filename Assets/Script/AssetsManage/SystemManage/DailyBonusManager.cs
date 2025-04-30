using System;
using System.Collections;
using System.Collections.Generic;
using TapTap.TapAd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 每日奖励
/// </summary>
public class DailyBonusManager : MonoBehaviour
{
    Button button;

    public TextMeshProUGUI time; // 标题
    readonly string buttonName = "看广告获取";

    // 广告位 id
    int id = 1041647;
    // 记录按钮上次点击的日期
    public DateTime lastClickDate;




    // Start is called before the first frame update
    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(TryWatchAdvertisement);
        // 初始化上次点击日期为一个较早的日期，确保游戏开始时按钮可用
        if (lastClickDate == null)
        {
            lastClickDate = DateTime.MinValue;
        }

    }

    // 每帧更新
    void Update()
    {
        DateTime now = DateTime.Now;
        if (now.Date > lastClickDate.Date)
        {
            // 冷却时间已过，显示默认名称
            time.text = buttonName;
            // 启用按钮
            button.interactable = true;
        }
        else
        {
            // 冷却时间未过，计算到明天 0 点的剩余时间
            DateTime tomorrow = now.Date.AddDays(1);
            TimeSpan remainingTime = tomorrow - now;
            time.text = $"{remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2} 后刷新";
            // 禁用按钮
            button.interactable = false;
        }
    }

    // 尝试看广告获取双倍
    void TryWatchAdvertisement()
    {
        DateTime now = DateTime.Now;
        if (now.Date > lastClickDate.Date)
        {
            // 冷却时间已过，播放广告
            WatchAdvertisement();
        }
        else
        {
            // 冷却时间未过，可添加提示逻辑，例如显示剩余冷却时间
            DateTime tomorrow = now.Date.AddDays(1);
            TimeSpan remainingTime = tomorrow - now;
            Debug.Log($"冷却时间未过，剩余时间: {remainingTime.Hours:D2}:{remainingTime.Minutes:D2}:{remainingTime.Seconds:D2}");
        }
    }

    // 看广告获取双倍
    void WatchAdvertisement()
    {
        Example.example.ShowRewardAd(WatchOver, WatchOverOnclick);

    }

    void WatchOver()
    {
        // 更新上次点击日期
        lastClickDate = DateTime.Now;
        // 这里可以添加根据奖励验证结果执行的逻辑，例如如果验证通过则给予用户相应奖励，否则提示用户失败原因等
        // 目前代码中没有具体实现逻辑
        LogManager.Instance.AddLog("成功领取每日奖励获取10重生水晶");
        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, 10,false);
    }
    //额外奖励
    void WatchOverOnclick()
    {
        // 更新上次点击时间
        LogManager.Instance.AddLog("成功领取额外每日奖励10重生水晶");
        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, 10,false);

    }

}