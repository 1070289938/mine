using System.Collections;
using System.Collections.Generic;
using TapTap.TapAd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 离线收益
/// </summary>
public class RevenueManager : MonoBehaviour
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


    //离线时间
    public TextMeshProUGUI timeText;
    Dictionary<ResourceType, double> resource;
    // 广告位 id
    int id = 1042441;
    // Start is called before the first frame update
    void Start()
    {
        drawDown.onClick.AddListener(offline);
        doubleClaim.onClick.AddListener(WatchAdvertisement);
    }

    // Update is called once per frame
    void Update()
    {

    }
    // 存储上一次调用的时间
    private static float lastInvokeTime = 0f;
    // 冷却时间，单位为秒
    private static float CooldownDuration = 0.5f;

    public void offline()
    {
        // 检查是否已经过了冷却时间
        if (Time.time - lastInvokeTime >= CooldownDuration)
        {
            // 执行你想要防止多次调用的代码
            ReceiveIncome();
            // 更新上次调用的时间
            lastInvokeTime = Time.time;
        }
        else
        {
            Debug.Log($"还在冷却中，剩余时间: {CooldownDuration - (Time.time - lastInvokeTime):F1} 秒");
        }

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
            ResourceManager.Instance.AddResource(res.Key, res.Value, false);
        }
        TipsManager.Instance.OnClickAll();

    }



    public void Install(Dictionary<ResourceType, double> resource, int time)
    {
        //转换时间格式

        timeText.text = Utils.FormatTime(time);


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
        Example.example.ShowRewardAd(WatchOver, WatchOverOnclick);

    }

    void WatchOver()
    {
        // 这里可以添加根据奖励验证结果执行的逻辑，例如如果验证通过则给予用户相应奖励，否则提示用户失败原因等
        // 目前代码中没有具体实现逻辑
        LogManager.Instance.AddLog("成功获得双倍离线收益:");
        foreach (var res in resource)
        {
            double val = res.Value * 2;
            LogManager.Instance.AddLog(res.Key.GetName() + "+" + AssetsUtil.FormatNumber(val));
            ResourceManager.Instance.AddResource(res.Key, val, false);
        }
        TipsManager.Instance.OnClickAll();
    }

    //额外奖励
    void WatchOverOnclick()
    {
        // 更新上次点击时间
        LogManager.Instance.AddLog("成功获得额外离线收益:");

        foreach (var res in resource)
        {
            double val = res.Value * 0.5;
            LogManager.Instance.AddLog(res.Key.GetName() + "+" + AssetsUtil.FormatNumber(val));
            ResourceManager.Instance.AddResource(res.Key, val, false);
        }
        TipsManager.Instance.OnClickAll();
    }
}
