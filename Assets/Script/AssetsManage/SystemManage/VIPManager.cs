using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 时间管理
/// </summary>
public class VIPManager : MonoBehaviour
{
    public GameObject Content; // 内容
    public long ExpiredTime; //过期时间


    public int countTime;//剩余秒时间

    // 是否开启VIP
    public bool vipFlag;

    public TextMeshProUGUI timeText; // 标题

    // 用于计时的变量
    public float timer = 0f;

    public static VIPManager Instance;

    /// <summary>
    /// 增加到期时间
    /// </summary>
    public void AddTime(int time)
    {
        if (ExpiredTime == 0)
        {
            ExpiredTime = time + LoadUtil.GetTimestampInMilliseconds(System.DateTime.Now)/1000;
        }
        else
        {
            ExpiredTime += time;
        }
    }

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        vipFlag = false;
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

            countTime = (int)(ExpiredTime - LoadUtil.GetTimestampInMilliseconds(System.DateTime.Now)/1000);
           
            if (countTime < 0)
            {
                ExpiredTime = 0;
                vipFlag = false;
                if (Content.activeSelf)
                {
                    Content.SetActive(false);
                }
            }
            else
            {
                vipFlag = true;
                if (!Content.activeSelf)
                {
                    Content.SetActive(true);
                }
            }


            // 更新时间文本显示
            UpdateTimeText();
        }
    }

    // 将 RemainingTime 转换为 00:00 格式并更新到 timeText
    private void UpdateTimeText()
    {
        timeText.text = Utils.FormatTime(countTime);
    }
}