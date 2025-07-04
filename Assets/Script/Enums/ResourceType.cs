using System;
using System.Reflection;


public enum ResourceType
{
  none, //没有东西
  /// <summary>
  /// 软妹币
  /// </summary>
  [DisplayName("软妹币")]
  Currency, // 软妹币
  /// <summary>
  /// 石矿
  /// </summary>
  [DisplayName("石矿")]
  Stone,  // 石矿
  /// <summary>
  /// 铜矿
  /// </summary>
  [DisplayName("铜矿")]
  Copper, // 铜矿
  /// <summary>
  /// 铁矿
  /// </summary>
  [DisplayName("铁矿")]
  Iron,   // 铁矿

  /// <summary>
  /// 水泥
  /// </summary>
  [DisplayName("水泥")]
  Cement,//水泥


  /// <summary>
  /// 煤矿
  /// </summary>
  [DisplayName("煤矿")]
  Colliery,//煤矿

  /// <summary>
  /// 钢
  /// </summary>
  [DisplayName("钢")]
  Steel,//钢


  /// <summary>
  /// 硅矿
  /// </summary>
  [DisplayName("硅矿")]
  Silicon,//硅矿



  /// <summary>
  /// 重生晶体
  /// </summary>
  [DisplayName("重生晶体")]
  RegeneratedCrystal,//重生晶体


  /// <summary>
  /// 铝矿
  /// </summary>
  [DisplayName("铝矿")]
  Aluminum,//铝矿


  /// <summary>
  /// 钛矿
  /// </summary>
  [DisplayName("钛矿")]
  Titanium,//钛矿


  /// <summary>
  /// 合金
  /// </summary>
  [DisplayName("合金")]
  Alloy,//合金

  /// <summary>
  /// 科技点
  /// </summary>
  [DisplayName("科技点")]
  Science,//科技点


  /// <summary>
  /// 佐里旬矿
  /// </summary>
  [DisplayName("佐里旬矿")]
  Zorizun,//佐里旬矿

  /// <summary>
  /// 银矿
  /// </summary>
  [DisplayName("银矿")]
  silver,


  /// <summary>
  /// 钨
  /// </summary>
  [DisplayName("钨")]
  Tungsten,

  /// <summary>
  /// 镍
  /// </summary>
  [DisplayName("镍")]
  Nickel,

  /// <summary>
  /// 纳米材料
  /// </summary>
  [DisplayName("纳米材料")]
  Nanomaterials,

  /// <summary>
  /// 地心岩
  /// </summary>
  [DisplayName("地心岩")]
  GeocentricRock,


  /// <summary>
  /// 四维宝石
  /// </summary>
  [DisplayName("四维宝石")]
  DimensionalStone,//四维宝石


  /// <summary>
  /// 铱
  /// </summary>
  [DisplayName("铱")]
  Iridium,//铱

  /// <summary>
  /// 秘银
  /// </summary>
  [DisplayName("秘银")]
  Mithril,//秘银


  /// <summary>
  /// 中子
  /// </summary>
  [DisplayName("中子")]
  Neutron,//中子



  /// <summary>
  /// 金属氢
  /// </summary>
  [DisplayName("金属氢")]
  MetallicHydrogen,//金属氢


  /// <summary>
  /// 耀斑矿
  /// </summary>
  [DisplayName("耀斑矿")]
  Flare,//耀斑矿

  /// <summary>
  /// 精金
  /// </summary>
  [DisplayName("精金")]
  Adamant,//精金


  /// <summary>
  /// 暮晶
  /// </summary>
  [DisplayName("暮晶")]
  MudCrystal,//暮晶

  /// <summary>
  /// 记忆合金
  /// </summary>
  [DisplayName("记忆合金")]
  memoryAlloy,//记忆合金


  /// <summary>
  /// 飞升精华
  /// </summary>
  [DisplayName("飞升精华")]
  AscensionEssence,//飞升精华
}



[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
sealed class DisplayNameAttribute : Attribute
{
  public string Name { get; }

  public DisplayNameAttribute(string name)
  {
    Name = name;
  }
}

public static class ResourceTypeExtensions
{
  public static string GetName(this ResourceType resourceType)
  {
    FieldInfo field = resourceType.GetType().GetField(resourceType.ToString());
    DisplayNameAttribute attribute = field.GetCustomAttribute<DisplayNameAttribute>();
    return attribute != null ? attribute.Name : resourceType.ToString();
  }

}




public static class ResourceTypeHelper
{
  // 将字符串转换为 TechType 枚举值
  public static ResourceType StringToResourceType(string value)
  {
    if (Enum.TryParse(value, out ResourceType techType))
    {
      return techType;
    }
    // 如果解析失败，返回默认值 empty
    return ResourceType.none;
  }

  // 将 TechType 枚举值转换为字符串
  public static string ResourceTypeToString(ResourceType techType)
  {
    return techType.ToString();
  }
}
