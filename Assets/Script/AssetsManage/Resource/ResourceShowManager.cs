using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空间
using UnityEngine.UI;
using System;
/// <summary>
/// 梁恩 v1.0
/// </summary>
// 资源显示管理
public class ResourceShowManager : MonoBehaviour
{
    // 资源名称
    [Header("资源属性")]
    string resourceName;

    // 当前资源数量
    double currentAmount;

    // 最大库存
    public double maxStorage;

    // 每秒生产量
    double productionRate;

    [Header("UI 组件")]
    // 名称显示的文本组件
    public TextMeshProUGUI resourceNameText;

    // 当前数量显示的文本组件
    public TextMeshProUGUI currentAmountText;

    // 最大库存显示的文本组件
    public TextMeshProUGUI maxStorageText;

    // 每秒生产显示的文本组件
    public TextMeshProUGUI productionRateText;

    public bool show = false;

    public ResourceType resourceType;

    public ResourceType GetResourceType()
    {
        return resourceType;
    }


    // 上一次计算资源产量的时间
    private float lastCalculationTime;

    private void Start()
    {
        SetMaxStorage(maxStorage); // 初始化库存
       

        // 默认隐藏
        gameObject.SetActive(show); // 隐藏节点

        lastCalculationTime = Time.time;
        InvokeRepeating("ComputeProduction", 1f, 1f);
    }

    // 上一秒资源数
    double lastResource;
    /// <summary>
    /// 重生初始化
    /// </summary>
    public void Initialize()
    {
        lastResource = 0;
        currentAmount = 0;
    }

    private void Update()
    {
        ShowResource(); // 显示资源
    }

    // 每帧进行资源的显示
    private void ShowResource()
    {
        // 获取石矿资源
        double count = ResourceManager.Instance.GetResource(resourceType);
        // 显示资源
        SetCurrentAmount(count);
    }

    // 计算产量
    void ComputeProduction()
    {
        // 计算实际的时间间隔
        float deltaTime = (Time.time - lastCalculationTime) / Time.timeScale;//计算时间缩放因子
        lastCalculationTime = Time.time;

        // 计算差量
        productionRate = (currentAmount - lastResource) / deltaTime;

        // 更新上一秒的资源数量
        lastResource = currentAmount;

        ResourceManager resource = ResourceManager.Instance;
        if (resource.buildExpend.ContainsKey(resourceType))
        {
            productionRate += resource.buildExpend[resourceType] / deltaTime;
            resource.buildExpend[resourceType] = 0;
        }

        SetProductionRate(productionRate);
    }

    // 设置名字
    public void SetName(string resourceName)
    {
        this.resourceName = resourceName;

        if (resourceNameText != null)
            resourceNameText.text = resourceName;
    }

    // 设置类型
    public void SetType(ResourceType resourceType)
    {
        this.resourceType = resourceType;
        SetName(resourceType.GetName());
    }

    public ResourceType getResourceType()
    {
        return resourceType;
    }

    // 设置现有资源
    public void SetCurrentAmount(double currentAmount)
    {
        this.currentAmount = currentAmount;
        if (currentAmountText != null)
        {
            currentAmountText.text = AssetsUtil.FormatNumber(Math.Truncate(currentAmount));
        }
    }

    // 设置最大资源
    public void SetMaxStorage(double maxStorage)
    {
        this.maxStorage = maxStorage;

        if (maxStorageText != null)
            maxStorageText.text = "/" + AssetsUtil.FormatNumber(Math.Truncate(maxStorage));
        // 同步缓存上限
        ResourceManager.Instance.SetResourceMax(resourceType, maxStorage);
    }

    // 设置每秒资源
    public void SetProductionRate(double productionRate)
    {
        this.productionRate = productionRate;

        if (productionRateText != null)
        {
            if (productionRate < 0)
            {
                // 负数处理
                productionRate = 0 - productionRate;
                productionRateText.text = "-" + AssetsUtil.FormatNumber(productionRate) + "/s";
                productionRateText.color = Color.red;
            }
            else
            {
                productionRateText.text = "+" + AssetsUtil.FormatNumber(productionRate) + "/s";
                productionRateText.color = new Color32(0, 191, 255, 255); // 薄荷绿色
            }
        }
    }
}