//------------------------------------------------------------------------------
// Copyright (c) 2018-2023 Beijing Bytedance Technology Co., Ltd.
// All Right Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using ByteDance.Union;
using ByteDance.Union.Mediation;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The example for the SDK.
/// </summary>
///
public sealed class Example : MonoBehaviour
{

    public RewardVideoAd rewardAd;               // 激励视频，支持csj和融合



    // Unity 主线程ID:
    public static int MainThreadId;
    public static int MNowPlayAgainCount = 0;
    public static int MNextPlayAgainCount = 0;

    public static bool useMediation = true;

    private void Awake()
    {

        MainThreadId = Thread.CurrentThread.ManagedThreadId;
    }

    private void SdkInitCallback(bool success, string message)
    {
        // 注意：在初始化回调成功后再请求广告
        Debug.Log("CSJM_Unity " + "sdk初始化结束：success: " + success + ", message: " + message);
        // 也可以调用sdk的函数，判断sdk是否初始化完成
        Debug.Log("CSJM_Unity " + "sdk是否初始化成功, IsSdkReady: " + Pangle.IsSdkReady());
    }

    void Start()
    {
        // sdk初始化
        SDKConfiguration sdkConfiguration = new SDKConfiguration.Builder()
            .SetAppId(CSJMDAdPositionId.APP_ID)
            .SetAppName("重生挖矿")
            .SetUseMediation(Example.useMediation) // 是否使用融合功能，置为false，可不初始化聚合广告相关模块
            .SetDebug(false) // debug日志开关，app发版时记得关闭
            .SetMediationConfig(GetMediationConfig())
            .SetPrivacyConfigurationn(GetPrivacyConfiguration())
            .SetAgeGroup(0)
            .SetPaid(false) // 是否是付费用户
            .SetTitleBarTheme(AdConst.TITLE_BAR_THEME_LIGHT) // 设置落地页主题
            .SetKeyWords("") // 设置用户画像关键词列表
            .Build();

        Pangle.Init(sdkConfiguration); // 合规要求，初始化分为2步，第一步先调用init
        Pangle.Start(SdkInitCallback); // 第二步再调用start。注意在初始化回调成功后再请求广告
    }

    /* 💖💖💖💖💖💖💖💖💖💖💖💖💖💖 ↓↓↓↓↓↓↓↓↓↓ 广告sdk初始化 及 其他设置相关 ↓↓↓↓↓↓↓↓↓↓ 💖💖💖💖💖💖💖💖💖💖💖💖💖💖 */

    /**
     * 初始化时进行隐私合规相关配置。不设置的将使用默认值
     */
    private PrivacyConfiguration GetPrivacyConfiguration()
    {
        // 这里仅展示了部分设置，开发者根据自己需要进行设置，不设置的将使用默认值，默认值可能不合规。
        PrivacyConfiguration privacyConfig = new PrivacyConfiguration();
        privacyConfig.CanUsePhoneState = false;
        privacyConfig.CanUseLocation = false;
        privacyConfig.Longitude = 115.7;
        privacyConfig.Latitude = 39.4;
        //privacyConfig.CustomIdfa = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";


        // 融合相关配置示例
        privacyConfig.MediationPrivacyConfig = new MediationPrivacyConfig();
        privacyConfig.MediationPrivacyConfig.LimitPersonalAds = false;
        privacyConfig.MediationPrivacyConfig.ProgrammaticRecommend = false;
        privacyConfig.MediationPrivacyConfig.forbiddenCAID = false;
        privacyConfig.MediationPrivacyConfig.CanUseOaid = false;

        return privacyConfig;
    }

    /**
     * 使用融合功能时，初始化时进行相关配置
     */
    private MediationConfig GetMediationConfig()
    {
        MediationConfig mediationConfig = new MediationConfig();

        // 聚合配置json字符串（从gromore平台下载），用于首次安装时作为兜底配置使用。可选
        mediationConfig.CustomLocalConfig = MediationLocalConfig.CONFIG_JSON_STR;

        // 流量分组功能，可选
        MediationConfigUserInfoForSegment segment = new MediationConfigUserInfoForSegment();
        segment.Age = 18;
        segment.Gender = AdConst.GENDER_MALE;
        segment.Channel = "mediation-unity";
        segment.SubChannel = "mediation-sub-unity";
        segment.UserId = "mediation-userId-unity";
        segment.UserValueGroup = "mediation-user-value-unity";
        segment.CustomInfos = new Dictionary<string, string>
        {
            { "customKey", "customValue" }
        };
        mediationConfig.MediationConfigUserInfoForSegment = segment;

        return mediationConfig;
    }

    /* 💖💖💖💖💖💖💖💖💖💖💖💖💖💖 ↑↑↑↑↑↑↑↑↑↑ 广告sdk初始化 及 其他设置相关 ↑↑↑↑↑↑↑↑↑↑ 💖💖💖💖💖💖💖💖💖💖💖💖💖💖 */


    /* 💛💛💛💛💛💛💛💛💛💛💛💛💛💛 ↓↓↓↓↓↓↓↓↓↓ 激励视频相关样例 ↓↓↓↓↓↓↓↓↓↓ 💛💛💛💛💛💛💛💛💛💛💛💛💛💛 */
    // Show the reward Ad.  最终调用该方法，播放广告
    public void ShowRewardAd(Action action)
    {
        ExampleRewardAd.LoadReward(this, action);
    }
    /* 💛💛💛💛💛💛💛💛💛💛💛💛💛💛 ↑↑↑↑↑↑↑↑↑↑ 激励视频相关样例 ↑↑↑↑↑↑↑↑↑↑ 💛💛💛💛💛💛💛💛💛💛💛💛💛💛 */


    // Dispose the reward Ad.
    public void DisposeAds()
    {
        // 激励
        if (this.rewardAd != null)
        {
            this.rewardAd.Dispose();
            this.rewardAd = null;
        }
    }
}
