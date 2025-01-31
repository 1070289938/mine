using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空间
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class FacilityPanelManager : MonoBehaviour
{
    // UI 元素引用
    [Header("UI References")]
    public TextMeshProUGUI nameText; // 标题
    public TextMeshProUGUI descriptionText; // 详情
    public TextMeshProUGUI quantityText; // 数量显示
    public Button miningButton; // 按钮

    public GameObject requirementCount;//价格内容

    public GameObject outputContent;//产出内容

    public GameObject inputContent;//消耗内容

    public GameObject requirement;//价格
    public GameObject output;//产出

    public GameObject input;//消耗

    public GameObject resourcePrefab;//资源预制体

    public bool show = false;//默认隐藏

    string resourceName;
    string resourceDescription;
    int resourceQuantity;//物体数量(正常)

    int operationQuantity;//运行数量

    bool quantityFlag = false;//可以修改运行数量(默认没有)

    string btnText;


    // 挖矿回调函数
    public delegate void OnMineAction();
    public OnMineAction onMineCallback;

    public Action press; //按钮事件

    Dictionary<ResourceType, double> resources; //按下按钮需要的资源

    public double resourcesUp = 0.2;// 每次建造资源的提升倍数 （默认0.2）

    public Button addButton;//增加按钮

    public Button decrease;//减少按钮

    public GameObject button;//按钮


    public FacilityType FacilityType;

    double upMultiple = 1; //资源递增的倍数默认是1


    public void Awake()
    {

    }

    /// <summary>
    /// 减少百分比的资源倍数
    /// </summary>
    /// <param name="decline"></param>
    public void AddUpMultiple(double decline)
    {
        upMultiple *= 1 - decline;//倍数乘以 1-倍数 = 最终倍数（例如0.1
        UpdateRequirements();//更新初始价格
    }

    private void Start()
    {
        gameObject.SetActive(show);
        miningButton.onClick.AddListener(OnMineButtonClicked);
        UpdateRequirements();//更新初始价格
    }
    /// <summary>
    /// 初始化按钮
    /// </summary>
    public void InstallButton()
    {
        quantityFlag = true;
        button.SetActive(true);//显示按钮
        addButton.onClick.AddListener(AddOperationQuantity);
        decrease.onClick.AddListener(DecreaseOperationQuantity);
    }
    /// <summary>
    /// 增加运行数量
    /// </summary>
    void AddOperationQuantity()
    {
        //如果运行数量小于物体实际数量就增加
        if (operationQuantity < resourceQuantity)
        {
            operationQuantity++;
            quantityText.text = "x" + operationQuantity + "/" + resourceQuantity;
        }
    }
    /// <summary>
    /// 减少运行数量
    /// </summary>
    void DecreaseOperationQuantity()
    {
        //如果运行数量大于0就减少
        if (operationQuantity > 0)
        {
            operationQuantity--;
            quantityText.text = "x" + operationQuantity + "/" + resourceQuantity;
        }
    }



    // 更新 UI 显示
    public void UpdateModuleUI()
    {
        nameText.text = resourceName;
        descriptionText.text = resourceDescription;
        quantityText.text = "x" + resourceQuantity;
        miningButton.GetComponentInChildren<TextMeshProUGUI>().text = btnText;

    }
    /// <summary>
    /// 隐藏价格
    /// </summary>
    public void hideRequirement()
    {
        requirement.SetActive(false);
    }
    /// <summary>
    /// 获取数量（实际上获取到的是运行数量）
    /// </summary>
    /// <returns></returns>
    public int GetCount()
    {
        // return resourceQuantity;//这个是最大数量
        return operationQuantity;//这是实际运行的数量
    }

    /// <summary>
    /// 数量+1
    /// </summary>
    public void AddQuantityUI()
    {

        resourceQuantity++;//最大数量和运行数量同时增加
        operationQuantity++;
        if (quantityFlag)
        {
            quantityText.text = "x" + operationQuantity + "/" + resourceQuantity;
        }
        else
        {
            quantityText.text = "x" + resourceQuantity;
        }

    }

    // 设置新的资源信息
    public void SetResource(string name, string description, int quantity, string btnText)
    {
        resourceName = name;
        resourceDescription = description;
        resourceQuantity = quantity;
        this.btnText = btnText;
        UpdateModuleUI();
    }
    /// <summary>
    /// 设置基础消耗
    /// </summary>
    /// <param name="resources"></param>
    public void SetOnClickedResource(Dictionary<ResourceType, double> resources)
    {
        this.resources = resources;
    }
    /// <summary>
    /// 增加基础消耗
    /// </summary>
    /// <param name="type"></param>
    /// <param name="count"></param>
    public void AddOnClickedResource(ResourceType type, double count)
    {
        resources[type] = count;
        UpdateRequirements();
    }

    // 点击按钮时触发
    private void OnMineButtonClicked()
    {
        Dictionary<ResourceType, double> expendResources = UpdateRequirements();//更新一下价格
        //更新所需资源

        //判断资源是否足够
        JudgmentResult result = ResourceManager.Instance.JudgmentResource(expendResources);
        if (!result.flag)
        {
            //资源不足,进行抖动
            ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[result.type];
            Animation animation = resourceShow.GetComponent<Animation>();
            if (!animation.isPlaying) // 如果没有动画在播放
            {
                animation.Play("assets_quiver");
            }
            return;
        }
        //执行按钮方法
        press.Invoke();
        UpdateRequirements();//更新价格
    }

    /// <summary>
    /// 更新消耗价格表
    /// </summary>
    /// <param name="expendResources">最新价格</param>
    private Dictionary<ResourceType, double> UpdateRequirements()
    {

        //最新价格
        Dictionary<ResourceType, double> expendResources = new Dictionary<ResourceType, double>();
        //实际最新价格=基础数值X增值百分比的.1+当前数量次方
        //每个资源都计算一下
        foreach (var reso in resources)
        {
            double count = reso.Value * Math.Pow(1 + resourcesUp * upMultiple, resourceQuantity);
            expendResources[reso.Key] = Math.Truncate(count);
        }
        // 删除当前节点下的所有子节点
        foreach (Transform child in requirementCount.transform)
        {
            Destroy(child.gameObject);
        }
        //重新赋值节点
        foreach (var res in expendResources)
        {
            GameObject resource = Instantiate(resourcePrefab, requirementCount.transform);
            ExpendManager expend = resource.GetComponent<ExpendManager>();
            expend.Install(res.Key, res.Value);
        }

        return expendResources;

    }
    //产出资源初始化
    public void OutPutInstall(Dictionary<ResourceType, double> resource)
    {
        output.SetActive(true);
        // 删除当前节点下的所有子节点
        foreach (Transform child in outputContent.transform)
        {
            Destroy(child.gameObject);
        }
        outPutresourceExpend = new Dictionary<ResourceType, ExpendManager>();
        //重新赋值节点
        foreach (var res in resource)
        {

            GameObject ex = Instantiate(resourcePrefab, outputContent.transform);
            ExpendManager expend = ex.GetComponent<ExpendManager>();
            outPutresourceExpend[res.Key] = expend;
            expend.Install(res.Key, res.Value, true);
        }
    }
    private Dictionary<ResourceType, ExpendManager> outPutresourceExpend;
    public void UpdateOutPut(ResourceType type, double count)
    {
        ExpendManager expend = outPutresourceExpend[type];
        expend.UpdateCount(count);
    }


    //消耗资源初始化
    public void InputInstall(Dictionary<ResourceType, double> resource)
    {
        input.SetActive(true);
        // 删除当前节点下的所有子节点
        foreach (Transform child in inputContent.transform)
        {
            Destroy(child.gameObject);
        }
        inputResourceExpend = new Dictionary<ResourceType, ExpendManager>();
        //重新赋值节点
        foreach (var res in resource)
        {

            GameObject ex = Instantiate(resourcePrefab, inputContent.transform);
            ExpendManager expend = ex.GetComponent<ExpendManager>();
            inputResourceExpend[res.Key] = expend;
            expend.Install(res.Key, res.Value, true);
        }
    }
    private Dictionary<ResourceType, ExpendManager> inputResourceExpend;


    public void UpdateInPut(ResourceType type, double count)
    {
        ExpendManager expend = inputResourceExpend[type];
        expend.UpdateCount(count);
    }

}
