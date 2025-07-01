using System;
using System.Threading;
using System.Threading.Tasks;
using TapSDK.Achievement;
using TapTap.TapAd;
using TapTap.TapAd.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

public sealed class TapAdUtils : MonoBehaviour
{



    [SerializeField]
    private TextMeshProUGUI infoText;

    private TapRewardVideoAd _tapRewardAd;

    public static TapAdUtils Instance;

    // Unity 主线程ID:
    private static int mainThreadId;

    private void Awake()
    {
        Instance = this;
        Init();
        RequestPermission();

    }

 

    //播放激励视频
    public void PlayRewardVideo(int adId, ICommonInteractionListener listener)
    {
        //先加载激励视频
        LoadRewardVideoAd(adId, listener);

    }

    private void ShowText(string content)
    {
        content = string.Format($"[Unity:TapAd] {content} | Time: {DateTime.Now.ToString("g")}");
        Debug.LogFormat(content);
    }

    private void Init()
    {
        TapAdConfig config = null;
        ICustomController customController = null;

        config = new TapAdConfig.Builder()
            .MediaId(1009675)
            .MediaName("重生之我被挖矿系统绑定了")
            .MediaKey("j4ATa4a05LYlmsdg3FQfU8QWNtAftWHacCooQax6Hv6jb9BDkIUAwuAx1dKfiIRC")
            .EnableDebugLog(true)
            .MediaVersion("1")
            .Channel("taptap")
            // .TapClientId("0RiAlMny7jiz086FaU")
            .Build();
        customController = new FormalCustomControllerWrapper(this);
        TapAdSdk.Init(config, customController);
        ShowText("初始化完毕");
    }

    private void RequestPermission()
    {
        if (TapAdSdk.IsInited == false)
        {
            ShowText("TapAd 需要先初始化!");
            return;
        }
        TapAdSdk.RequestPermissionIfNecessary();
        ShowText("请求权限");
    }

    private async void LoadRewardVideoAd(int adId, ICommonInteractionListener listener)
    {
        if (TapAdSdk.IsInited == false)
        {
            ShowText("TapAd 没有初始化!将自动初始化");
            Init();
            while (TapAdSdk.IsInited == false)
            {
                await Task.Yield();
            }
            ShowText("TapAd 初始化完毕");
        }
        if (_tapRewardAd != null)
        {
            _tapRewardAd.Dispose();
            _tapRewardAd = null;
        }
        // create AdRequest
        var request = new TapAdRequest.Builder()
            .SpaceId(adId)
            .Extra1("{}")
            .RewardName("UnityRewardName")
            .RewardCount(9)
            .UserId("123")
            .Build();
        _tapRewardAd = new TapRewardVideoAd(request);
        _tapRewardAd.SetLoadListener(new RewardVideoAdLoadListener(this, listener));
        _tapRewardAd.Load();
    }

    private void PlayRewardVideoAd(ICommonInteractionListener listener)
    {
        if (TapAdSdk.IsInited == false)
        {
            ShowText("TapAd 需要先初始化!");
            return;
        }
        if (_tapRewardAd != null)
        {
            _tapRewardAd.SetInteractionListener(listener);
            _tapRewardAd.Show();
        }
        else
        {
            Debug.LogErrorFormat($"[Unity::AD] 未加载好视频,无法播放!");
        }
    }



    private void OnClickUserActionButton()
    {
        var userActions = new UserAction[3];

        var jan1st1970 = new DateTime
            (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        for (int i = 0; i < 3; i++)
        {
            var tmp = new UserAction(actionType: i, actionTime: (long)(DateTime.UtcNow - jan1st1970).TotalMilliseconds,
                amount: i * 1000, winStatus: i % 2);
            userActions[i] = tmp;
        }
        TapAdSdk.UploadUserAction(userActions, new CustomUserAction(this));
    }

    public sealed class CustomUserAction : IUserAction
    {
        private readonly TapAdUtils example;

        /// <summary>
        /// constructor bind with Java interface
        /// </summary>
        /// <param name="context"></param>
        public CustomUserAction(TapAdUtils context)
        {
            example = context;
        }

        public void OnSuccess()
        {
            example.ShowText($"上报成功");
        }

        public void OnError(int code, string message)
        {
            example.ShowText($"上报失败 code: {code} message: {message}");
        }
    }

    public sealed class FormalCustomControllerWrapper : ICustomController
    {
        private readonly TapAdUtils example;

        /// <summary>
        /// constructor bind with Java interface
        /// </summary>
        /// <param name="context"></param>
        public FormalCustomControllerWrapper(TapAdUtils context)
        {
            example = context;
        }

        public bool CanUseLocation => false;

        public TapAdLocation GetTapAdLocation => null;

        public bool CanUsePhoneState => false;
        public string GetDevImei => "";
        public bool CanUseWifiState => false;
        public bool CanUseWriteExternal => false;
        public string GetDevOaid => null;
        public bool Alist => false;
        public bool CanUseAndroidId => false;
        public CustomUser ProvideCustomer() => null;
    }
    /// <summary>
    /// 激励广告加载
    /// </summary>
    public sealed class RewardVideoAdLoadListener : IRewardVideoAdLoadListener
    {
        private readonly TapAdUtils example;

        private readonly ICommonInteractionListener listener;
        /// <summary>
        /// constructor bind with Java interface
        /// </summary>
        /// <param name="context"></param>
        public RewardVideoAdLoadListener(TapAdUtils context, ICommonInteractionListener listener)
        {
            example = context;
            this.listener = listener;
        }

        public void OnError(int code, string message)
        {
            message = message ?? "NULL";
            Debug.LogErrorFormat($"加载激励视频错误! 错误 code: {code} message: {message}");
        }

        public void OnRewardVideoAdCached(TapRewardVideoAd ad)
        {
            example.ShowText($"{ad.AdType}素材 Cached 完毕! ad != null: {(ad != null).ToString()} Thread On MainThread: {Thread.CurrentThread.ManagedThreadId == mainThreadId}");
            Assert.IsTrue(ad.IsReady, "Cached ad.IsReady");
        }

        public void OnRewardVideoAdLoad(TapRewardVideoAd ad)
        {
            example.PlayRewardVideoAd(listener);
        }
    }




}
