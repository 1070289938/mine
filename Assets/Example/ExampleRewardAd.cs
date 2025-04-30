using System;
using System.Collections.Generic;
using System.Threading;
using ByteDance.Union;
using ByteDance.Union.Mediation;
using UnityEngine;

/**
 * 激励视频代码示例。
 * 注：该接口支持融合功能
 */
public class ExampleRewardAd
{


    // 加载广告
    public static void LoadReward(Example example, Action action, Action onClickAction)
    {
        // 释放上一次广告
        if (example.rewardAd != null)
        {
            example.rewardAd.Dispose();
            example.rewardAd = null;
        }

        // 竖屏
        var codeId = CSJMDAdPositionId.M_REWARD_VIDEO_ID;
        // 创造广告参数对象
        var adSlot = new AdSlot.Builder()
            .SetCodeId(codeId) // 必传
            .SetUserID(Utils.GetUserId()) // 用户id,必传参数
            .SetOrientation(AdOrientation.Vertical) // // 必填参数，期望视频的播放方向  Vertical:竖屏   Horizontal: 横屏
            .Build();
        // 加载广告
        SDK.CreateAdNative().LoadRewardVideoAd(adSlot, new RewardVideoAdListener(example, action, onClickAction));
    }

    // 展示广告
    public static void ShowReward(Example example, Action action, Action onClickAction)
    {
        if (example.rewardAd == null)
        {
            Debug.LogError("CSJM_Unity " + "请先加载广告");
            // example.information.text = "请先加载广告";
        }
        else
        {
            // 设置展示阶段的监听器
            example.rewardAd.SetRewardAdInteractionListener(new RewardAdInteractionListener(example, action, onClickAction));
            example.rewardAd.SetAgainRewardAdInteractionListener(new RewardAgainAdInteractionListener(example));
            example.rewardAd.SetDownloadListener(new AppDownloadListener(example));
            example.rewardAd.SetAdInteractionListener(new TTAdInteractionListener());
#if UNITY_ANDROID
            example.rewardAd.SetRewardPlayAgainController(new RewardAdPlayAgainController());
#endif
            example.rewardAd.ShowRewardVideoAd();
        }
    }


    /**
     * 广告加载监听器
     */
    public sealed class RewardVideoAdListener : IRewardVideoAdListener
    {
        private Example example;

        private Action action;
        private Action onClickAction;
        public RewardVideoAdListener(Example example, Action action, Action onClickAction)
        {
            this.example = example;
            this.action = action;
            this.onClickAction = onClickAction;
        }

        public void OnError(int code, string message)
        {
            Debug.LogError("CSJM_Unity " + $"OnRewardError:{message} on main thread:{Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            Debug.LogError("CSJM_Unity " + example.rewardAd.GetMediationManager().GetAdLoadInfo());
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = "OnRewardError: " + message;
            // }
        }

        public void OnRewardVideoAdLoad(RewardVideoAd ad)
        {
            Debug.Log("CSJM_Unity " + $"OnRewardVideoAdLoad on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // Debug.Log(this.example);
            // Debug.Log(this.example.information);
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = "OnRewardVideoAdLoad";
            // }
            this.example.rewardAd = ad;
        }

        public void OnRewardVideoCached()
        {
            Debug.Log("CSJM_Unity " + $"OnRewardVideoCached on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId) this.example.information.text = "OnRewardVideoCached";
        }

        public void OnRewardVideoCached(RewardVideoAd ad)
        {

            ShowReward(this.example, action, onClickAction);
            Debug.Log("CSJM_Unity " + $"OnRewardVideoCached RewardVideoAd ad on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
        }
    }

    // 广告展示监听器
    public sealed class RewardAdInteractionListener : IRewardAdInteractionListener
    {

        // 存储上一次调用的时间
        private static float lastInvokeTime = 0f;
        // 冷却时间，单位为秒
        private static float CooldownDuration = 0.5f;


        private Example example;

        private Action action;
        Action onClickAction;
        public RewardAdInteractionListener(Example example, Action action, Action onClickAction)
        {
            this.example = example;
            this.action = action;
            this.onClickAction = onClickAction;
        }

        public void OnAdShow()
        {
            TipsManager.Instance.HideBackLoad();

            // 检查是否已经过了冷却时间
            if (Time.time - lastInvokeTime >= CooldownDuration)
            {
                // 执行你想要防止多次调用的代码
                action?.Invoke();
                // 更新上次调用的时间
                lastInvokeTime = Time.time;
            }
            else
            {
                Debug.Log($"还在冷却中，剩余时间: {CooldownDuration - (Time.time - lastInvokeTime):F1} 秒");
            }
            flag = true;
            LogMediationInfo(example);
        }

        bool flag = false;

        public void OnAdVideoBarClick()
        {
            if (flag)
            {
                flag = false;
                onClickAction?.Invoke();
            }

        }

        public void OnAdClose()
        {
            Debug.Log("CSJM_Unity " + $"rewardVideoAd close on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = "rewardVideoAd close";
            // }

            if (this.example.rewardAd != null)
            {
                this.example.rewardAd.Dispose();
                this.example.rewardAd = null;
            }
        }

        public void OnVideoSkip()
        {
            Debug.Log("CSJM_Unity " + $"rewardVideoAd skip on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = "rewardVideoAd skip";
            // }
        }

        public void OnVideoComplete()
        {

        }

        public void OnVideoError()
        {
            Debug.LogError("CSJM_Unity " + $"rewardVideoAd error on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = "rewardVideoAd error";
            // }
        }

        public void OnRewardArrived(bool isRewardValid, int rewardType, IRewardBundleModel extraInfo)
        {
            var logString = "OnRewardArrived verify:" + isRewardValid + " rewardType:" + rewardType + " extraInfo: " + extraInfo.ToString() +
                            $" on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}";
            Debug.Log("CSJM_Unity " + logString);
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = logString;
            // }
        }
    }

    // 广告再看一个监听器
    public sealed class RewardAdPlayAgainController : IRewardAdPlayAgainController
    {
        public void GetPlayAgainCondition(int nextPlayAgainCount, Action<PlayAgainCallbackBean> callback)
        {
            Debug.Log("CSJM_Unity " + "Reward GetPlayAgainCondition");
            Example.MNextPlayAgainCount = nextPlayAgainCount;
            var bean = new PlayAgainCallbackBean(true, "金币", nextPlayAgainCount + "个");
            callback?.Invoke(bean);
        }
    }

    // 广告再看一个监听器
    public sealed class RewardAgainAdInteractionListener : IRewardAdInteractionListener
    {
        private Example example;

        public RewardAgainAdInteractionListener(Example example)
        {
            this.example = example;
        }

        public void OnAdShow()
        {
            Debug.Log("CSJM_Unity " + $"again rewardVideoAd show on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            string msg = "Callback --> 第 " + Example.MNowPlayAgainCount + " 次再看 rewardPlayAgain show";
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = msg;
            // }
        }

        public void OnAdVideoBarClick()
        {
            Debug.Log("CSJM_Unity " + $"again rewardVideoAd bar click on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text =
            //         "Callback --> 第 " + Example.MNowPlayAgainCount + " 次再看 rewardPlayAgain bar click";
            // }
        }

        public void OnAdClose()
        {
            Debug.Log("CSJM_Unity " + "OnAdClose");
        }

        public void OnVideoSkip()
        {
            Debug.Log("CSJM_Unity " + $"again rewardVideoAd skip on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = "Callback --> 第 " + Example.MNowPlayAgainCount + " 次再看 rewardPlayAgain has OnVideoSkip";
            // }
        }

        public void OnVideoComplete()
        {
            Debug.Log("CSJM_Unity " + $"again rewardVideoAd complete on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = "Callback --> 第 " + Example.MNowPlayAgainCount + " 次再看 rewardPlayAgain complete";
            // }
        }

        public void OnVideoError()
        {
            Debug.LogError("CSJM_Unity " + $"again rewardVideoAd error on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}");
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = "Callback --> 第 " + Example.MNowPlayAgainCount + " 次再看 rewardPlayAgain error";
            // }
        }

        public void OnRewardArrived(bool isRewardValid, int rewardType, IRewardBundleModel extraInfo)
        {
            var logString = "again OnRewardArrived verify:" + isRewardValid + " rewardType:" + rewardType + " extraInfo:" + extraInfo +
                            $" on main thread: {Thread.CurrentThread.ManagedThreadId == Example.MainThreadId}";
            Debug.Log("CSJM_Unity " + logString);
            // if (Thread.CurrentThread.ManagedThreadId == Example.MainThreadId)
            // {
            //     this.example.information.text = logString;
            // }
        }
    }

    // 打印广告相关信息
    private static void LogMediationInfo(Example example)
    {
        MediationAdEcpmInfo showEcpm = example.rewardAd.GetMediationManager().GetShowEcpm();
        if (showEcpm != null)
        {
            LogUtils.LogMediationAdEcpmInfo(showEcpm, "GetShowEcpm");
        }

        MediationAdEcpmInfo bestEcpm = example.rewardAd.GetMediationManager().GetBestEcpm();
        if (bestEcpm != null)
        {
            LogUtils.LogMediationAdEcpmInfo(bestEcpm, "GetBestEcpm");
        }

        List<MediationAdEcpmInfo> multiBiddingEcpmList = example.rewardAd.GetMediationManager().GetMultiBiddingEcpm();
        foreach (MediationAdEcpmInfo item in multiBiddingEcpmList)
        {
            LogUtils.LogMediationAdEcpmInfo(item, "GetMultiBiddingEcpm");
        }

        List<MediationAdEcpmInfo> cacheList = example.rewardAd.GetMediationManager().GetCacheList();
        foreach (MediationAdEcpmInfo item in cacheList)
        {
            LogUtils.LogMediationAdEcpmInfo(item, "GetCacheList");
        }

        List<MediationAdLoadInfo> adLoadInfoList = example.rewardAd.GetMediationManager().GetAdLoadInfo();
        foreach (MediationAdLoadInfo item in adLoadInfoList)
        {
            LogUtils.LogAdLoadInfo(item);
        }
    }
}
