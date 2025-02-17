using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class LogManager : MonoBehaviour
{
    public static LogManager Instance { get; private set; }

    public TextMeshProUGUI logText; // 用于显示日志的 Text 组件
    public ScrollRect scrollRect; // 滚动视图，用于自动滚动
    public List<string> logs;
    public int maxLogCount = 100; // 最大日志数量

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (logText == null || scrollRect == null)
        {
            Debug.LogError("logText or scrollRect is not assigned!");
            return;
        }

        // 强制滚动到底部
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }

    // 添加日志
    public void AddLog(string message)
    {
        if (logText == null || scrollRect == null)
        {
            Debug.LogError("logText or scrollRect is not assigned!");
            return;
        }

        if (logs == null)
        {
            logs = new List<string>();
        }

        // 检查日志数量是否超过最大限制
        if (logs.Count >= maxLogCount)
        {
            int removeCount = logs.Count - maxLogCount + 1;
            for (int i = 0; i < removeCount; i++)
            {
                logs.RemoveAt(0); // 移除最早的日志
            }
            // 重新构建日志文本
            logText.text = string.Join("\n", logs);
        }

        // 添加日志到列表
        logs.Add(message);

        // 只追加新的日志内容
        if (logText.text.Length > 0)
        {
            logText.text += "\n" + message;
        }
        else
        {
            logText.text = message;
        }

        // 自动滚动到最新日志
        ScrollToBottom();
    }

    private void ScrollToBottom()
    {
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }
}