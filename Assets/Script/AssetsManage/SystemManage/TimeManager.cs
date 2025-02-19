using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 时间管理
/// </summary>
public class TimeManager : MonoBehaviour
{
    public GameObject Content; // 内容
    public int RemainingTime; // 秒

    // 存储游戏的总时间速度
    private float gameTimeScale;

    public TextMeshProUGUI timeText; // 标题

    // 用于计时的变量
    public float timer = 0f;

    public static TimeManager Instance;

    /// <summary>
    /// 增加时间
    /// </summary>
    public void AddTime(int time)
    {
        RemainingTime += time;
    }

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 初始化游戏时间速度
        gameTimeScale = 1f;

        // 初始显示时间
        UpdateTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        // 累加计时器
        timer += Time.unscaledDeltaTime;

        // 判断是否过去了一秒
        if (timer >= 1f)
        {
            // 重置计时器
            timer = 0f;

            // 根据 RemainingTime 的值调整游戏时间速度
            if (RemainingTime == 0)
            {
                gameTimeScale = 1f;
                if (Content.activeSelf)
                {
                    Content.SetActive(false);
                }
            }
            else
            {
                gameTimeScale = 2f;
                if (!Content.activeSelf)
                {
                    Content.SetActive(true);
                }
            }

            // 设置游戏时间速度
            Time.timeScale = gameTimeScale;

            // 如果 RemainingTime 大于 0，则将其减一
            if (RemainingTime > 0)
            {
                RemainingTime--;
            }

            // 更新时间文本显示
            UpdateTimeText();
        }
    }

    // 将 RemainingTime 转换为 00:00 格式并更新到 timeText
    private void UpdateTimeText()
    {
        int minutes = RemainingTime / 60;
        int seconds = RemainingTime % 60;
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}