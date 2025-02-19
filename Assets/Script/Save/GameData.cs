using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public Dictionary<ResourceType, double> resources;  //各个资源的数量
    public Dictionary<ResourceType, double> resourcesMax;//各个资源的上限
    public Dictionary<ResourceType, bool> resourceUnlocks;//各个资源是否显示
    public Dictionary<string, bool> StudyFlag;//科技是否研究
    public Dictionary<FacilityType, FacilityPanelCount> facility;//面板拥有数量
    public List<string> logs;//日志


    public float speedTime;//加速时间


    public long dailyBonus;//每日奖励上次点击时间

    public long productionAcceleration;//双倍加速奖励上次点击事件


}