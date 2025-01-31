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
    StoneHammer,//石质大锤
    CopperHammer,//铜质大锤
    IronWarehouse,//铁质仓库
    ConcreteBuilding,//水泥房屋
    shore,//支撑柱
    StrengthenWarehouse,//加强仓库
    painted,//画饼
    BrokenStone,//破开石头

    //==============================================矿场===================================================


}


public class ExampleScript : MonoBehaviour
{
    public ResourceType resourceType;
}

#if UNITY_EDITOR

// 自定义 Inspector
[CustomEditor(typeof(ExampleScript))]
public class ExampleScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ExampleScript script = (ExampleScript)target;

        // 获取枚举类型
        var enumType = typeof(ResourceType);
        var enumValues = Enum.GetValues(enumType);

        // 获取描述
        string[] descriptions = new string[enumValues.Length];
        for (int i = 0; i < enumValues.Length; i++)
        {
            var field = enumType.GetField(enumValues.GetValue(i).ToString());
            var descriptionAttr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            descriptions[i] = descriptionAttr != null ? descriptionAttr.Description : field.Name;
        }

        // 显示下拉菜单
        int selectedIndex = (int)script.resourceType;
        selectedIndex = EditorGUILayout.Popup("资源类型", selectedIndex, descriptions);

        // 更新枚举值
        script.resourceType = (ResourceType)selectedIndex;

        // 显示描述提示
        EditorGUILayout.HelpBox(descriptions[selectedIndex], MessageType.Info);

        EditorUtility.SetDirty(script); // 标记对象已更改
    }
}
#endif