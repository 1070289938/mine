using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapSDK.Core;
using TapSDK.Login;
using System;
using UnityEngine.SceneManagement;

public class TapInstall : MonoBehaviour
{
    //客户端id
    private string clientId = "wcstctdngmqzpuadpz";
    //token
    private string clientToken = "kbucKgtQr3HWcoCC6rVVyr8tx6u8FHci8e9Of5tw";

    // Start is called before the first frame update
    void Awake()
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
                // 用户已登录
                Debug.Log("用户已登录..直接进入游戏");
                SceneManager.LoadScene("GameScene");
            }
        }
        catch (Exception e)
        {
            Debug.Log($"获取用户信息失败 {e.Message}");
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
