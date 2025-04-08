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
