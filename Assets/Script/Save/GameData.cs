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

}