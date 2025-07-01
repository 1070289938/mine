using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using static ExchangeManager;

public class PostManager : MonoBehaviour
{

    public static PostManager Instance { get; private set; }

    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }


    public void ToPost(string url, string json, Action<int, string> action)
    {
        StartCoroutine(Post(url, json, action));
    }

    IEnumerator Post(string url, string json, Action<int, string> action)
    {
        // 要发送POST请求的API的URL
        string apiUrl = url;

        // 要发送的数据（这里使用JSON格式）
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST"))
        {
            // 设置请求体
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            // 设置请求头
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // 发送请求并等待响应
            yield return webRequest.SendWebRequest();

            // 检查请求是否成功
            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // 获取响应文本
                string responseText = webRequest.downloadHandler.text;
                action?.Invoke(0, responseText);
                Debug.Log("Response: " + responseText);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                TapData root = JsonUtility.FromJson<TapData>(webRequest.downloadHandler.text);
                Debug.Log(root.message);
                // 输出错误信息
                Debug.Log("Error: " + webRequest.error);
                LogManager.Instance.AddLog(root.info.hint);
                action?.Invoke(1, webRequest.error);
            }
        }
    }
}


// 定义JSON根对象类
[System.Serializable]
public class TapData
{
    public int error;
    public string message;

    public InfoData info;
}

[System.Serializable]
public class InfoData
{
    public string hint;

}
