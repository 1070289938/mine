using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

public class NetworkTimeApiHelper
{
    public static DateTime GetNetworkTimeFromApi()
    {
        try
        {
            // 定义 API 请求的 URL
            string url = "http://worldtimeapi.org/api/ip";
            // 创建一个 HTTP 请求
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            // 获取响应
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                // 读取响应内容
                string json = reader.ReadToEnd();
                // 解析 JSON 数据
                var timeData = JsonConvert.DeserializeObject<TimeData>(json);
                // 将时间字符串转换为 DateTime 类型
                return DateTime.Parse(timeData.datetime);
            }
        }
        catch (Exception ex)
        {
            // 若出现异常，打印错误信息并返回当前系统时间作为备用
            Console.WriteLine($"获取 API 时间时出错: {ex.Message}");
            return DateTime.Now;
        }
    }

    // 定义一个类来存储从 API 获取的时间数据
    public class TimeData
    {
        public string datetime;
    }
}