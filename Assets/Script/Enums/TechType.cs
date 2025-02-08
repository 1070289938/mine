using System;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using System.Reflection;


public enum TechType
{
    //=============================================矿洞====================================================
    /// <summary>
    /// 别管他,没有用
    /// </summary>
    empty, //无研究
    pickaxe, //石镐
    warehouse,  // 仓库
    house, // 房屋
    minerlamp,//矿灯
    BrassPick,//铜镐
    CopperWarehouse,//铜制仓库
    collectRents,//收租
    SimpleDecoration,//简易的家具
    SimpleFinish,//简陋的装修
    StoneMiner,//石矿工人
    CopperMiner,//铜矿工人
    SaveMoney,//储钱罐
    MoneyBox,//储钱罐
    IronMiner,//铁矿工人
    CementManufacture,//水泥生产
    rail,//铁轨
    tramcar,//矿车
    ironPickaxe,//铁镐
    tupid,//铁质大锤
    Chisel,//凿子
    StoneHammer,//石质大锤
    CopperHammer,//铜质大锤
    IronWarehouse,//铁质仓库
    ConcreteBuilding,//水泥房屋
    shore,//支撑柱
    StrengthenWarehouse,//加强仓库
    painted,//画饼

    FindLight,//发现光芒

    BrokenStone,//破开石头

    //==============================================矿场===================================================


}
public static class TechTypeHelper
{
    // 将字符串转换为 TechType 枚举值
    public static TechType StringToTechType(string value)
    {
        if (Enum.TryParse(value, out TechType techType))
        {
            return techType;
        }
        // 如果解析失败，返回默认值 empty
        return TechType.empty;
    }

    // 将 TechType 枚举值转换为字符串
    public static string TechTypeToString(TechType techType)
    {
        return techType.ToString();
    }
}
