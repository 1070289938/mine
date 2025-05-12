using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 科技对象
/// </summary>
public class TechnologyBean
{

    public int id;//id
    /// <summary>
    /// 科技标题
    /// </summary>
    public string studyTitle;
    public string studyName;//科技名字

    public string details;//科技描述

    public string successful;//研究完成描述

    public int stage;//科技的阶段

    /// <summary>
    /// 研究需要的资源
    /// </summary>
    public Dictionary<ResourceType, double> resources;

    public List<ResourceType> advanceResources;//前置资源

    public List<TechType> advanceTechType;//前置科技

    public List<TechType> monitorTechType;//后置监听
    /// <summary>
    /// 收益
    /// </summary>
    public Dictionary<string, double> revenue;//收益

    /// <summary>
    /// 科技类型
    /// </summary>
    public TechType techType;

    public int type;


}
