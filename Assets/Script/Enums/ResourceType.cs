using System;
using System.Reflection;


public enum ResourceType
{    none, //没有东西
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
