using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceOverManager : MonoBehaviour
{
    //游戏开头开场白
    string gameStart = "你重生到了挖矿世界\n并且被重生系统绑定了\n你的每次死亡都会被记录\n并且重生到平行世界";

    //分解金属棒重生
    string decomposingRest = "那巨型金属棒被强行分解的瞬间\n仿佛触发了命运的诅咒\n矿洞毫无征兆地坍塌\n黑暗如潮水般将我和所有的一切吞噬\n我在绝望中坠入无尽的黑暗深渊\n然而，当我再次睁开双眼\n熟悉的疼痛却被陌生又熟悉的环境取代\n..............我重生了";


    public static VoiceOverManager Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    /// <summary>
    /// 显示开场旁白
    /// </summary>
    public void GameStartVoiceOver()
    {
        Show(gameStart, 0, null);
    }




    /// <summary>
    /// 显示加载中
    /// </summary>
    public void GameLoadVoiceOver()
    {
        Show("加载中.............", 2, null);
    }
    /// <summary>
    /// 矿洞坍塌导致重生了
    /// </summary>
    public void DecomposingRestVoiceOver(Action action)
    {
        Show(decomposingRest, 5, action);
    }

    /// <summary>
    /// 挖爆星球导致重生了
    /// </summary>
    public void DiggingPlanetsLeadsRebirthOver(Action action)
    {
        Show(decomposingRest, 5, action);
    }



    /// <summary>
    /// 显示
    /// </summary>
    public void Show(string str, float time, Action action)
    {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 1;

        gameObject.SetActive(true);
        TextRevealMultiLine.Instance.StartReveal(str, gameObject, time, action);
    }


}
