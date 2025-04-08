using System.Collections;
using System.Collections.Generic;
using TapTap.TapAd;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 离线收益
/// </summary>
public class RevenueManager : MonoBehaviour, IRewardVideoInteractionListener
{
    /// <summary>
    /// 收益内容
    /// </summary>
    public GameObject content;

    /// <summary>
    /// 收益预制体
    /// </summary>
    public GameObject expendFab;

    //领取按钮
    public Button drawDown;

    //双倍领取按钮
    public Button doubleClaim;

    public Example example;
    Dictionary<ResourceType, double> resource;
    // 广告位 id
    int id = 1042441;
    // Start is called before the first frame update
    void Start()
    {
        drawDown.onClick.AddListener(ReceiveIncome);
        doubleClaim.onClick.AddListener(WatchAdvertisement);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 领取离线收益
    /// </summary>
    public void ReceiveIncome()
    {
        LogManager.Instance.AddLog("成功获得离线收益:");
        foreach (var res in resource)
        {
            LogManager.Instance.AddLog(res.Key.GetName() + "+" + AssetsUtil.FormatNumber(res.Value));
            ResourceManager.Instance.AddResource(res.Key, res.Value);
        }
        TipsManager.Instance.OnClickAll();

    }



    public void Install(Dictionary<ResourceType, double> resource)
    {
        this.resource = resource;
        //销毁所有的子节点
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject); // 销毁子物体
        }
        // 使用 foreach 遍历字典的键值对
        foreach (var res in resource)
        {
            GameObject expend = Instantiate(expendFab, content.transform);
            expend.GetComponent<ExpendManager>().Install(res.Key, res.Value);
        }



    }


    // 看广告获取双倍
    void WatchAdvertisement()
    {
        // example.ShowRewardAd(WatchOver);
        TapAdUtils.Instance.PlayRewardVideo(id, this);
    }
    //////////////////////////////////////////////////////接口方法//////////////////////////////////////////////////////


    void WatchOver()
    {
        // 这里可以添加根据奖励验证结果执行的逻辑，例如如果验证通过则给予用户相应奖励，否则提示用户失败原因等
        // 目前代码中没有具体实现逻辑
        LogManager.Instance.AddLog("成功获得双倍离线收益:");
        foreach (var res in resource)
        {
            double val = res.Value * 2;
            LogManager.Instance.AddLog(res.Key.GetName() + "+" + AssetsUtil.FormatNumber(val));
            ResourceManager.Instance.AddResource(res.Key, val);
        }
        TipsManager.Instance.OnClickAll();
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
        WatchOver();
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
