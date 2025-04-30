using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapSDK.Core;
using TapSDK.Login;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class TapInstall : MonoBehaviour
{
    //客户端id
    private string clientId = "wcstctdngmqzpuadpz";
    //token
    private string clientToken = "kbucKgtQr3HWcoCC6rVVyr8tx6u8FHci8e9Of5tw";

    public Button loginButton;

    // Start is called before the first frame update
    void Start()
    {
        // 核心配置
        TapTapSdkOptions coreOptions = new TapTapSdkOptions
        {
            // 客户端 ID，开发者后台获取
            clientId = clientId,
            // 客户端令牌，开发者后台获取
            clientToken = clientToken,
            // 地区，CN 为国内，Overseas 为海外
            region = TapTapRegionType.CN,
            // 语言，默认为 Auto，默认情况下，国内为 zh_Hans，海外为 en
            preferredLanguage = TapTapLanguageType.zh_Hans,
            // 是否开启日志，Release 版本请设置为 false
            enableLog = true
        };
        // TapSDK 初始化
        TapTapSDK.Init(coreOptions);


        // 当需要添加其他模块的初始化配置项，例如合规认证、成就等， 请使用如下 API
        TapTapSdkBaseOptions[] otherOptions = new TapTapSdkBaseOptions[]
        {
            // 其他模块配置项
        };
        TapTapSDK.Init(coreOptions, otherOptions);

        JudgmentLanding();
        loginButton.onClick.AddListener(Login);
        DontDestroyOnLoad(gameObject);
    }
    /// <summary>
    /// 判断是否登陆
    /// </summary>
    async void JudgmentLanding()
    {


        try
        {
            TapTapAccount account = await TapTapLogin.Instance.GetCurrentTapAccount();
            if (account == null)
            {
                // 用户未登录
                Debug.Log("用户未登录");
            }
            else
            {
                Debug.Log(account.ToJson());
                // 用户已登录
                Debug.Log("用户已登录..");
                // 存储数据
                LoginBean loginBean = new()
                {
                    name = account.name,
                    tapId = account.unionId,
                    tapGameId = account.openId
                };
                Utils.SetUserId(account.unionId);
                SceneManager.LoadScene("GameScene");
            }
        }
        catch (Exception e)
        {
            Debug.Log($"获取用户信息失败 {e.Message}");
        }


    }

    /// <summary>
    /// 登陆事件
    /// </summary>
    async void Login()
    {
        try
        {
            // 定义授权范围
            List<string> scopes = new List<string>
            {
                TapTapLogin.TAP_LOGIN_SCOPE_PUBLIC_PROFILE
            };
            // 发起 Tap 登录
            var userInfo = await TapTapLogin.Instance.LoginWithScopes(scopes.ToArray());
            Debug.Log($"登录成功，当前用户 ID：{userInfo.unionId}...进入游戏");
            // 存储数据
            LoginBean loginBean = new()
            {
                name = userInfo.name,
                tapId = userInfo.unionId,
                tapGameId = userInfo.openId
            };
            Utils.SetUserId(userInfo.unionId);
            SceneManager.LoadScene("GameScene");


        }
        catch (TaskCanceledException)
        {
            Debug.Log("用户取消登录");
        }
        catch (Exception exception)
        {
            Debug.Log($"登录失败，出现异常：{exception}");
        }
    }




}
