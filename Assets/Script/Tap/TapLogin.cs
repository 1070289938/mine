using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TapSDK.Login;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TapLogin : MonoBehaviour
{

    public Button loginButton;

    // Start is called before the first frame update
    void Awake()
    {
        loginButton.onClick.AddListener(Login);
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

    // Update is called once per frame
    void Update()
    {

    }
}
