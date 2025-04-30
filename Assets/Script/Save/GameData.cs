using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏存档对象
/// </summary>
[Serializable]
public class GameData
{
    public bool secondLife;//是否为重生存档

    public Dictionary<ResourceType, double> resources;  //各个资源的数量
    public Dictionary<ResourceType, double> resourcesMax;//各个资源的上限
    public Dictionary<ResourceType, bool> resourceUnlocks;//各个资源是否显示
    public Dictionary<string, bool> StudyFlag;//科技是否研究
    public Dictionary<FacilityType, FacilityPanelCount> facility;//面板拥有数量
    public List<string> logs;//日志


    public float speedTime;//加速时间


    public long dailyBonus;//每日奖励上次点击时间

    public long productionAcceleration;//双倍加速奖励上次点击事件
    public long AccelerateImmediately;//立即加速上次点击时间
    public long saveTime;//保存时间

    public int militaryStrength;//当前兵力

    public int attackStrength;//出击兵力

    public int dangerValue;//当前危险值
    public bool autofill;//自动填充
    public int secondLifeCount;//重生次数


    public int marsPoints;//火星制造点数


}