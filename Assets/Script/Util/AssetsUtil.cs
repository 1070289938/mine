using System;
using System.Globalization;

public class AssetsUtil
{
    // 单位缩写列表
    private static readonly string[] unitSuffixes = { "", "K", "M", "G", "T", "P", "E", "Z", "Y", "B", "N", "D", "C" };

    /// <summary>
    /// 格式化数值为带单位的字符串
    /// </summary>
    /// <param name="value">需要格式化的数值</param>
    /// <returns>带单位的字符串</returns>
    public static string FormatNumber(double value)
    {
        // 如果数值为 0，直接返回 "0"
        if (value == 0)
            return "0";

        // 如果数值大于等于 10,000，使用单位缩写
        if (value >= 10000)
        {
            int unitIndex = 0;
            double absValue = Math.Abs(value);

            // 计算单位索引
            while (absValue >= 1000 && unitIndex < unitSuffixes.Length - 1)
            {
                absValue /= 1000;
                unitIndex++;
            }

            // 格式化数值并附加单位
            return $"{absValue:0.##}{unitSuffixes[unitIndex]}";
        }
        else
        {
            // 对于小于 10,000 的数值，去除无用的小数点
            return value.ToString("0.##");
        }
    }

    /// <summary>
    /// 将带单位或不带单位的字符串解析为数值
    /// </summary>
    /// <param name="formattedValue">带单位或不带单位的字符串（如 "1.5M" 或 "1500000"）</param>
    /// <returns>解析后的数值</returns>
    public static double ParseNumber(string formattedValue)
    {
        // 如果没有匹配单位，则尝试直接解析为普通数值
        if (double.TryParse(formattedValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
        {
            return result;
        }

        // 遍历单位缩写列表，匹配单位
        for (int i = unitSuffixes.Length - 1; i >= 0; i--)
        {
            if (formattedValue.EndsWith(unitSuffixes[i], StringComparison.OrdinalIgnoreCase))
            {
                // 去掉单位部分，解析数值
                string numberPart = formattedValue.Substring(0, formattedValue.Length - unitSuffixes[i].Length).Trim();
                if (double.TryParse(numberPart, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                {
                    return number * Math.Pow(1000, i);
                }
            }
        }

        throw new FormatException("无法解析输入的格式化数值: " + formattedValue);
    }
}
