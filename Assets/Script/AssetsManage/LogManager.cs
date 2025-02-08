using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System.Collections;

public class LogManager : MonoBehaviour
{


    public static LogManager Instance { get; private set; }

    public TextMeshProUGUI logText; // 用于显示日志的 Text 组件
    public ScrollRect scrollRect; // 滚动视图，用于自动滚动
    public List<string> logs;




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
        // 强制滚动到底部
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }

    // 添加日志
    public void AddLog(string message)
    {
        if (logs == null)
        {
            logs = new List<string>();
        }
        // 添加日志到列表
        logs.Add(message);

        // 更新 Text 的内容
        logText.text = string.Join("\n", logs);

        // 自动滚动到最新日志


        StartCoroutine(ScrollToBottom());
    }
    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}
