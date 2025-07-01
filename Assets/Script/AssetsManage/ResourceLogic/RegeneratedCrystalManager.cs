using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 重生晶体的管理
/// </summary>
public class RegeneratedCrystalManager : MonoBehaviour
{
    // 用于计时的变量
    private float timer = 0f;

    public float addition = 1;


    int secondLifeCount = 0;


    public static RegeneratedCrystalManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public int GetSecondLifeCount()
    {
        return secondLifeCount;
    }

    /// <summary>
    /// 设置重生次数
    /// </summary>
    /// <param name="count"></param>
    public void SetSecondLifeCount(int count)
    {
        secondLifeCount = count;
        if (secondLifeCount >= 1)
        {
            AchievementUtils.Unlock(Achievement.Reborn);
        }

        if (secondLifeCount >= 10)
        {
            AchievementUtils.Unlock(Achievement.RebirthExpert);
        }
        if (secondLifeCount >= 20)
        {
            AchievementUtils.Unlock(Achievement.awake);
        }
        if (secondLifeCount >= 50)
        {
            AchievementUtils.Unlock(Achievement.NotDying);
        }
        if (secondLifeCount >= 100)
        {
            AchievementUtils.Unlock(Achievement.wantDie);
        }
        if (secondLifeCount >= 200)
        {
            AchievementUtils.Unlock(Achievement.UnableDie);
        }


    }

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        // 初始化计时器
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 累加计时器
        timer += Time.deltaTime;

        // 判断是否过去了一秒
        if (timer >= 1f)
        {
            // 重置计时器
            timer = 0f;

            // 调用计算资源加成的方法，这里假设传入值为 250
            int inputValue = (int)ResourceManager.Instance.GetResource(ResourceType.RegeneratedCrystal);
            double result = CalculateResult(inputValue);

            // 可以在这里添加处理计算结果的逻辑，例如更新加成值
            addition = 1 + (float)result;

            // 示例：输出计算结果
            Debug.Log($"资源加成计算结果: {result:F5}，当前加成值: {addition:F5}");

            // 将加成值转换为百分比形式并设置给 text 组件
            float percentage = (addition - 1) * 100;
            text.text = $"提升: {percentage:F2}%产量";

            ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();

        }
    }

    /// <summary>
    /// 计算资源加成
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    static double CalculateResult(int value)
    {
        int max = 250;
        max += (int)ResourceManager.Instance.GetResource(ResourceType.DimensionalStone);

        if (value > max)
        {
            value = max;
        }
        // 计算 (ln(value + 50) - 3.91202) / 2.888
        return (Math.Log(value + 50) - 3.91202) / 2.888;

        // return 1000;
    }
}