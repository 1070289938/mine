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
    public TextMeshProUGUI count; // 领取次数
    readonly string buttonName = "看广告获取";
    readonly string timeFormat = "{0:D2}:{1:D2}:{2:D2} 后刷新";
    readonly string countFormat = "今日可领:{0}次";


    // 记录上次刷新的日期
    public DateTime lastRefreshDate;
    int maxClaimsPerDay = 1; // 每天最多可领取次数
    public int remainingClaims; // 剩余可领取次数


    public bool start = false;//如果是true代表存档加载完毕

    void Start()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(TryWatchAdvertisement);

        // 更新UI显示
        UpdateUI();
    }

    void Update()
    {
        // 每帧检查是否需要刷新
        CheckRefresh();

        // 更新UI显示
        UpdateUI();
    }

    // 尝试看广告获取奖励
    void TryWatchAdvertisement()
    {
        if (remainingClaims > 0)
        {
            // 还有剩余次数，播放广告
            WatchAdvertisement();
        }
        else
        {
            // 次数已用完，显示剩余时间
            Debug.Log("今日次数已用完");
            UpdateUI();
        }
    }

    // 看广告获取奖励
    void WatchAdvertisement()
    {
        // WatchOver();
        Example.example.ShowRewardAd(WatchOver, WatchOverOnclick);
    }

    // 广告播放完成回调
    void WatchOver()
    {
        // 减少可领取次数
        remainingClaims--;

        // 更新UI
        UpdateUI();

        // 发放奖励
        LogManager.Instance.AddLog("成功领取每日奖励获取10重生水晶");
        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, 10, false);
    }

    // 额外奖励回调
    void WatchOverOnclick()
    {
        LogManager.Instance.AddLog("成功领取额外每日奖励10重生水晶");
        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, 10, false);
    }

    // 检查是否需要刷新可领取次数
    void CheckRefresh()
    {
        Debug.Log("启用状态" + start);
        if (!start)
        {
            return;
        }
        DateTime now = DateTime.Now;
        Debug.Log(now.Date);
        Debug.Log(lastRefreshDate.Date);

        // 如果是新的一天，重置可领取次数
        if (now.Date > lastRefreshDate.Date)
        {
            Debug.Log("重置每日奖励领取次数");
            remainingClaims = maxClaimsPerDay;
            lastRefreshDate = now;


        }
    }

    // 更新UI显示
    void UpdateUI()
    {
        if (remainingClaims > 0)
        {
            // 还有剩余次数
            time.text = buttonName;
            count.text = string.Format(countFormat, remainingClaims);
            button.interactable = true;
        }
        else
        {
            // 次数已用完，显示到次日0点的剩余时间
            DateTime tomorrow = DateTime.Now.Date.AddDays(1);
            TimeSpan remainingTime = tomorrow - DateTime.Now;
            time.text = string.Format(timeFormat,
                remainingTime.Hours, remainingTime.Minutes, remainingTime.Seconds);
            count.text = string.Format(countFormat, 0);
            button.interactable = false;
        }
    }





}