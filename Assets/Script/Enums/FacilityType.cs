using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public enum FacilityType
{


    none, //没有东西
    /// <summary>
    /// 一堆矿石
    /// </summary>
    Ore,
    /// <summary>
    /// 仓库
    /// </summary>
    Stash,
    /// <summary>
    /// 房屋
    /// </summary>
    Tenement,
    /// <summary>
    /// 石矿工人
    /// </summary>
    StoneMiner,
    /// <summary>
    /// 铜矿工人
    /// </summary>
    CopperMiner,
    /// <summary>
    /// 铁矿工人
    /// </summary>
    IronMiner,
    /// <summary>
    /// 水泥搅拌工
    /// </summary>
    CementWorker,
    /// <summary>
    /// 铁轨
    /// </summary>
    Rail,
    /// <summary>
    /// 矿车
    /// </summary>
    OreCar,

}

