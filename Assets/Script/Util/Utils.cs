using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Utils
{
    // 将十六进制颜色代码转换为Color对象
    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("#", "");
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }


    public static void SetUserId(string userId)
    {
        PlayerPrefs.SetString("userId", userId);
        // 保存更改
        PlayerPrefs.Save();
    }
    public static string GetUserId()
    {
        return PlayerPrefs.GetString("userId");
    }
    /// <summary>
    /// 将object转为对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <returns></returns>
    public static T ConvertToType<T>(object data) where T : class
    {
        string jsonData;
        if (data is Dictionary<string, object> dict)
        {
            // 如果是字典类型，手动构建 JSON 字符串
            jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
        }
        else
        {
            // 尝试使用反射获取可序列化的属性并转换
            var serializedData = new Dictionary<string, object>();
            var type = data.GetType();
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead)
                {

                    object value = property.GetValue(data);
                    if (value != null)
                    {
                        serializedData[property.Name] = value;
                    }

                }
            }
            jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(serializedData);
        }

        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonData);
    }



    /// <summary>
    /// 将秒数转换为"时:分:秒"格式（00:00:00）
    /// </summary>
    /// <param name="totalSeconds">总秒数</param>
    /// <returns>格式化后的时间字符串</returns>
    public static string FormatTime(int totalSeconds)
    {
        // 计算小时、分钟、秒
        int hours = Mathf.FloorToInt(totalSeconds / 3600);
        int minutes = Mathf.FloorToInt((totalSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(totalSeconds % 60);

        // 使用ToString("00")确保数字为两位，不足则补零
        if (hours > 0)
        {
            // 有小时数时，显示完整的"时:分:秒"
            return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
        else
        {
            // 没有小时数时，只显示"分:秒"
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}



