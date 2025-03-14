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

    string keepDiggingDown = "随着挖掘工作的持续推进，深入星球核心的钻头已经触碰到了星球的极限\n在那深邃的地底，复杂的能量场开始紊乱，一场前所未有的危机正在悄然降临\n瞬间，整个星球的引力场失衡，星球表面的山脉、海洋开始剧烈动荡\n巨大的海啸掀起了数百米高的巨浪，将一切都淹没在汹涌的波涛之中\n高耸的山脉在剧烈的震动中崩塌，化作了漫天的尘埃\n紧接着，星球内部的核聚变反应失控，巨大的能量爆发将星球炸得粉碎\n无数的碎片在太空中高速飞行，如同一颗颗致命的子弹\n在这毁灭性的灾难面前，你被强大的冲击力所吞噬，意识陷入了无尽的黑暗\n然而，当我再次睁开双眼\n熟悉的疼痛却被陌生又熟悉的环境取代\n..............我重生了";
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
        Show(keepDiggingDown, 5, action);
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
