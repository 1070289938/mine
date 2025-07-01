using System.Collections;
using System.Collections.Generic;
using TapSDK.Achievement;
using UnityEngine;

public class AchievementCallback : ITapAchievementCallback
{
    public AchievementCallback() { }

    public void OnAchievementSuccess(int code, TapAchievementResult result)
    {
        // 成就状态更新成功
        // code 70001 解锁成就成功
        // code 70002 增加步长成功
        // result 成就数据详情
        
    }

    public void OnAchievementFailure(string achievementId, int errorCode, string errorMsg)
    {
        // 成就状态更新失败或其他错误
        // achievementId 触发失败的成就 ID， 如果调用的是 [ShowAchievements] 接口，则为 "" 空字符串。
        // errorCode 错误码
        // errorMsg 错误描述
        Debug.LogError("成就错误:"+errorMsg);
    }
}
