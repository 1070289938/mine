using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

// 一堆矿石
public class PileOfOreManager : MonoBehaviour
{

    Button miningButton; // 挖矿按钮

    UtilManager utilManager;

    FacilityPanelManager facilityPanelManager;

    // 模块的资源属性
    [Header("Module Properties")]
    string resourceName = "一堆矿石";
    string resourceDescription = "这是一堆矿石，挖挖看应该总会有点有用的东西";
    int resourceQuantity = 1;

    string btnText = "挖";

    // Start is called before the first frame update
    void Awake()
    {
        facilityPanelManager = GetComponent<FacilityPanelManager>();
        //绑定按钮
        miningButton = facilityPanelManager.miningButton;
        //全局工具管理
        utilManager = FindObjectOfType<UtilManager>();
        facilityPanelManager.SetResource(resourceName, resourceDescription, resourceQuantity, btnText);
        facilityPanelManager.SetOnClickedResource(new Dictionary<ResourceType, double>());//挖矿不需要资源
        facilityPanelManager.press = OnMineButtonClicked;
        facilityPanelManager.hideRequirement();//隐藏价格

    }

    void Start()
    {
        AddMethod(Rock, 100);

    }


    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 挖到石头
    /// </summary>
    void Rock()
    {

        double count = 1;
        count *= ResourceAdditionManager.Instance.GetToolUp();//加上工具的提升
        LogManager.Instance.AddLog("你挖到了石头!");
        ResourceManager.Instance.AddResource(ResourceType.Stone, count); // 增加石矿

    }

    public void AddCopper(){
        AddMethod(Copper,20);
    }

    /// <summary>
    /// 挖到铜
    /// </summary>
    void Copper()
    {
        double count = 1;
        count *= ResourceAdditionManager.Instance.GetToolUp();//加上工具的提升
        LogManager.Instance.AddLog("你挖到了铜矿!");
        ResourceManager.Instance.AddResource(ResourceType.Copper, count); // 增加铜矿
    }
    public void AddIron(){
        AddMethod(Iron,5);
    }
    /// <summary>
    /// 挖到铁
    /// </summary>
    void Iron()
    {
        double count = 1;
        count *= ResourceAdditionManager.Instance.GetToolUp();//加上工具的提升
        LogManager.Instance.AddLog("你挖到了铁矿!");
        ResourceManager.Instance.AddResource(ResourceType.Iron, count); // 增加铁矿
    }

    // 点击挖矿按钮时触发
    private void OnMineButtonClicked()
    {
        ExecuteMethod();
    }
    List<Action> methods = new List<Action>();
    List<float> weights = new List<float>();




    // 向列表中添加方法和权重
    public void AddMethod(Action method, float weight)
    {
        methods.Add(method);
        weights.Add(weight);
    }

    // 执行加权随机选择的方法
    void ExecuteMethod()
    {
        if (methods.Count == 0)
        {
            Debug.LogWarning("没有添加任何方法，无法执行。");
            return;
        }

        float totalWeight = 0;
        foreach (float weight in weights)
        {
            totalWeight += weight;
        }

        float randomValue = UnityEngine.Random.value * totalWeight;
        float cumulativeWeight = 0;
        for (int i = 0; i < methods.Count; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue <= cumulativeWeight)
            {
                if (methods[i] != null)
                {
                    methods[i]();
                }
                return;
            }
        }
    }
}
