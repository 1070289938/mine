using System;
using System.Collections;
using System.Collections.Generic;
using TapTap.TapAd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 双倍时间奖励
/// </summary>
public class ProductionAccelerationManager : MonoBehaviour, IRewardVideoInteractionListener
{
    Button button;

    public TextMeshProUGUI time; // 标题
    readonly string buttonName = "看广告获取";

    // 广告位 id
    int id = 1041644;
    // 记录按钮上次点击的时间
    public DateTime lastClickTime;
    // 冷却时间，单位：小时
    const int cooldownHours = 3;

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

    // 看广告时间加速
    void WatchAdvertisement()
    {
        TapAdUtils.Instance.PlayRewardVideo(id, this);
    }

    /// <summary>
    /// 点击事件
    /// </summary>
    bool OnclickFlag = false;

    /// <summary>
    /// 当奖励验证结果返回时触发此方法
    /// </summary>
    /// <param name="ad">触发此事件的 TapRewardVideoAd 广告实例</param>
    /// <param name="rewardVerify">表示奖励是否验证通过的布尔值</param>
    /// <param name="rewardAmount">奖励的数量</param>
    /// <param name="rewardName">奖励的名称</param>
    /// <param name="code">奖励验证结果的状态码</param>
    /// <param name="msg">奖励验证结果的消息说明</param>
    public void OnRewardVerify(TapRewardVideoAd ad, bool rewardVerify, int rewardAmount, string rewardName, int code, string msg)
    {
        // 更新上次点击时间
        lastClickTime = DateTime.Now;

        // 这里可以添加根据奖励验证结果执行的逻辑，例如如果验证通过则给予用户相应奖励，否则提示用户失败原因等
        // 目前代码中没有具体实现逻辑
        LogManager.Instance.AddLog("成功领取双倍时间流速30分钟");
        TimeManager.Instance.AddTime(1800);
       
    }

    /// <summary>
    /// 当激励视频广告被用户点击时触发此方法
    /// </summary>
    /// <param name="ad">触发此事件的 TapRewardVideoAd 广告实例</param>
    public void OnAdClick(TapRewardVideoAd ad)
    {
        if (!OnclickFlag)
        {

            OnclickFlag = true;
        }

        // 使用 Debug.LogErrorFormat 方法输出一条错误级别的日志信息
        // 日志内容为 "激励视频 点击"，用于记录用户点击激励视频广告的操作
        Debug.LogErrorFormat($"激励视频 点击");
    }

    /// <summary>
    /// 当激励视频广告展示时触发此方法
    /// </summary>
    /// <param name="ad">触发此事件的 TapRewardVideoAd 广告实例</param>
    public void OnAdShow(TapRewardVideoAd ad)
    {
        OnclickFlag = false;
        // 这里可以添加当广告显示时需要执行的逻辑，例如记录广告展示数据、暂停游戏等
        // 目前代码中仅作为显示广告的标识，没有具体实现逻辑
        // 显示广告
    }

    /// <summary>
    /// 当激励视频广告关闭时触发此方法
    /// </summary>
    /// <param name="ad">触发此事件的 TapRewardVideoAd 广告实例</param>
    public void OnAdClose(TapRewardVideoAd ad)
    {
        // 这里可以添加当广告关闭时需要执行的逻辑，例如恢复游戏、更新界面等
        // 目前代码中仅作为关闭广告的标识，没有具体实现逻辑
        // 关闭广告
    }

    /// <summary>
    /// 当激励视频广告完整播放完成时触发此方法
    /// </summary>
    /// <param name="ad">触发此事件的 TapRewardVideoAd 广告实例</param>
    public void OnVideoComplete(TapRewardVideoAd ad)
    {
        // 这里可以添加当视频播放完成时需要执行的逻辑，例如给予用户奖励、更新用户数据等
        // 目前代码中没有具体实现逻辑
    }

    /// <summary>
    /// 当激励视频广告播放出现错误时触发此方法
    /// </summary>
    /// <param name="ad">触发此事件的 TapRewardVideoAd 广告实例</param>
    public void OnVideoError(TapRewardVideoAd ad)
    {
        // 这里可以添加当视频播放出错时需要执行的逻辑，例如显示错误提示、重试加载广告等
        // 目前代码中没有具体实现逻辑
    }

    /// <summary>
    /// 当激励视频广告被用户跳过播放时触发此方法
    /// </summary>
    /// <param name="ad">触发此事件的 TapRewardVideoAd 广告实例</param>
    public void OnSkippedVideo(TapRewardVideoAd ad)
    {
        // 这里可以添加当视频被跳过时需要执行的逻辑，例如提示用户未获得奖励、记录用户跳过行为等
        // 目前代码中没有具体实现逻辑
    }
}