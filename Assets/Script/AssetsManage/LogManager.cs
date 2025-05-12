using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using TMPro;

public class LogManager : MonoBehaviour
{
    public static LogManager Instance { get; private set; }

    public TextMeshProUGUI logText; // 用于显示日志的 Text 组件
    public ScrollRect scrollRect; // 滚动视图，用于自动滚动
    public LinkedList<string> logs;
    public int maxLogCount = 100; // 最大日志数量
    private StringBuilder logBuilder;
    private bool needScrollToBottom;
    private readonly object _logLock = new object(); // 用于线程同步

    public void Initialize()
    {
        lock (_logLock)
        {
            if (logs == null)
            {
                logs = new LinkedList<string>();
            }
            logs.Clear();
            logBuilder = new StringBuilder();
            logText.text = "";
            ScrollToBottom();
        }
    }

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
        if (!IsUIComponentsAssigned())
        {
            return;
        }

        // 强制滚动到底部
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }

    // 检查 UI 组件是否已赋值
    private bool IsUIComponentsAssigned()
    {
        if (logText == null || scrollRect == null)
        {
            Debug.LogError("logText or scrollRect is not assigned!");
            return false;
        }
        return true;
    }

    // 添加日志
    public void AddLog(string message)
    {
        if (string.IsNullOrEmpty(message))
            return;

        if (!IsUIComponentsAssigned())
        {
            return;
        }

        lock (_logLock)
        {
            if (logs == null)
            {
                logs = new LinkedList<string>();
            }
            
            if (logBuilder == null)
            {
                logBuilder = new StringBuilder();
            }

            // 安全地移除最早的日志
            while (logs.Count >= maxLogCount && logs.First != null)
            {
                logBuilder.Remove(0, logs.First.Value.Length + 1); // 移除最早的日志文本
                logs.RemoveFirst(); // 移除最早的日志
            }

            // 添加日志到列表
            logs.AddLast(message);

            // 追加新的日志内容
            if (logBuilder.Length > 0)
            {
                logBuilder.AppendLine(message);
            }
            else
            {
                logBuilder.Append(message);
            }

            logText.text = logBuilder.ToString();

            // 标记需要滚动到底部
            needScrollToBottom = true;
        }
    }

    // 添加警告日志，显示为红色
    public void AddWarnLog(string message)
    {
        string coloredMessage = "<color=red>" + message + "</color>";
        AddLog(coloredMessage);
    }

    /// <summary>
    /// 重新加载日志
    /// </summary>
    public void Reset()
    {
        lock (_logLock)
        {
            if (logs == null)
            {
                logs = new LinkedList<string>();
                logBuilder = new StringBuilder();
            }

            logBuilder.Clear();
            foreach (var log in logs)
            {
                if (logBuilder.Length > 0)
                {
                    logBuilder.AppendLine(log);
                }
                else
                {
                    logBuilder.Append(log);
                }
            }
            
            if (logText != null) // 防止UI组件已被销毁
            {
                logText.text = logBuilder.ToString();
            }
        }
    }

    private void Update()
    {
        if (needScrollToBottom && scrollRect != null) // 防止UI组件已被销毁
        {
            ScrollToBottom();
            needScrollToBottom = false;
        }
    }

    private void ScrollToBottom()
    {
        if (scrollRect == null || scrollRect.content == null) // 防御性检查
            return;
            
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    /// <summary>
    /// 获取所有日志
    /// </summary>
    /// <returns>包含所有日志的列表</returns>
    public List<string> GetAllLogs()
    {
        lock (_logLock)
        {
            return new List<string>(logs ?? new LinkedList<string>());
        }
    }

    /// <summary>
    /// 加载所有日志
    /// </summary>
    /// <param name="logList">要加载的日志列表</param>
    public void LoadAllLogs(List<string> logList)
    {
        if (!IsUIComponentsAssigned())
        {
            return;
        }

        lock (_logLock)
        {
            if (logs == null)
            {
                logs = new LinkedList<string>();
            }
            logs.Clear();
            logBuilder = new StringBuilder();

            if (logList == null)
                return;

            foreach (string log in logList)
            {
                if (string.IsNullOrEmpty(log))
                    continue;

                // 安全地移除最早的日志
                while (logs.Count >= maxLogCount && logs.First != null)
                {
                    logBuilder.Remove(0, logs.First.Value.Length + 1);
                    logs.RemoveFirst();
                }

                // 添加日志到列表
                logs.AddLast(log);

                // 追加新的日志内容
                if (logBuilder.Length > 0)
                {
                    logBuilder.AppendLine(log);
                }
                else
                {
                    logBuilder.Append(log);
                }
            }

            logText.text = logBuilder.ToString();
            needScrollToBottom = true;
        }
    }
}