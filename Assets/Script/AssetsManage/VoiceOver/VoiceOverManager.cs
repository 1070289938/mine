using System;
using UnityEngine;

public class VoiceOverManager : MonoBehaviour
{
    //游戏开头开场白
    string gameStart = "你重生到了挖矿世界\n并且被重生系统绑定了\n你的每次死亡都会被记录\n并且重生到平行世界";

    //分解金属棒重生
    string decomposingRest = "那巨型金属棒被强行分解的瞬间\n仿佛触发了命运的诅咒\n矿洞毫无征兆地坍塌\n黑暗如潮水般将我和所有的一切吞噬\n我在绝望中坠入无尽的黑暗深渊\n然而，当我再次睁开双眼\n熟悉的疼痛却被陌生又熟悉的环境取代\n..............我重生了";

    string keepDiggingDown = "随着挖掘工作的持续推进，深入星球核心的钻头已经触碰到了星球的极限\n在那深邃的地底，复杂的能量场开始紊乱，一场前所未有的危机正在悄然降临\n瞬间，整个星球的引力场失衡，星球表面的山脉、海洋开始剧烈动荡\n巨大的海啸掀起了数百米高的巨浪，将一切都淹没在汹涌的波涛之中\n高耸的山脉在剧烈的震动中崩塌，化作了漫天的尘埃\n紧接着，星球内部的核聚变反应失控，巨大的能量爆发将星球炸得粉碎\n无数的碎片在太空中高速飞行，如同一颗颗致命的子弹\n在这毁灭性的灾难面前，你被强大的冲击力所吞噬，意识陷入了无尽的黑暗\n然而，当我再次睁开双眼\n熟悉的疼痛却被陌生又熟悉的环境取代\n..............我重生了";

    // 破坏四维传送门胜利重生
    string destroyPortalWin = "你拼尽全力，终于成功破坏了四维传送门\n刹那间，传送门破碎，爆发出一股强大到无法想象的能量，如汹涌的潮水般席卷整个星球\n这股能量所到之处，一切都在发生着不可思议的变化\n我们那仅剩的军队，还有被这股能量波及到的所有生物，都在痛苦与挣扎中，神奇地完成了转化\n它们的身体逐渐变形，毛发变得坚硬而粗糙，牙齿变得尖锐无比，最终都变成了一头头凶猛的地心犬\n然而，这股强大的能量也超出了你的承受极限，你感觉自己的身体在逐渐瓦解，意识也开始模糊\n在生命的最后一刻，你看着那些新生的地心犬，心中五味杂陈\n随着最后一丝意识的消散，你坠入了无尽的黑暗\n当你再次睁开双眼，熟悉的疼痛却被陌生又熟悉的环境取代\n..............我重生了。";

    // 破坏四维传送门失败重生
    string destroyPortalLoss = "我们的大部队过于轻敌，严重低估了对方的实力\n在与地心犬的激烈战斗中，我方士兵节节败退，最终全军覆没\n杀红了眼的狂暴地心犬们，如同决堤的洪水一般，全部都冲出了大门\n它们所到之处，生灵涂炭，整个星球陷入了一片血海之中\n城市被摧毁，森林被焚烧，无数的生命在这场灾难中消逝\n你在这场浩劫中，也未能幸免，被地心犬的攻击所吞噬，意识渐渐模糊\n在绝望中，你看着这满目疮痍的星球，心中充满了悔恨\n最终，黑暗完全将你笼罩，你的意识陷入了无尽的深渊\n当你再次睁开双眼，熟悉的疼痛却被陌生又熟悉的环境取代\n..............我重生了。";

    // 测试实验型探索者后因空间坍缩重生
    string testExplorerCollapseRebirth = "你怀着对未知的渴望，毅然启动了实验型探索者\n引擎的轰鸣声仿佛是命运的号角，却未曾料到，这竟是一场灾难的开端\n突然，引擎核心处爆发出刺目的强光\n紧接着便是一阵令人心悸的扭曲声响空间开始以引擎为中心疯狂坍缩。\n那坍缩的空间如同一头贪婪的巨兽，以极快的速度吞噬着周围的一切\n恒星被无情地扯入其中，光芒瞬间熄灭\n化作无尽黑暗中的一点幻影\n行星被强大的引力撕裂，碎片如流星般四散飞溅\n整个太阳系在这股恐怖的力量下，如同脆弱的玻璃，瞬间支离破碎。\n你被这突如其来的变故吓得魂飞魄散\n身体被强大的引力拉扯，仿佛要被撕成碎片。\n剧烈的疼痛让你几乎昏厥，但求生的本能让你强撑着意识\n然而，在这绝对的力量面前，一切挣扎都是徒劳\n你眼睁睁地看着太阳系在眼前消失\n自己也被黑暗彻底吞噬，意识陷入了无尽的深渊。\n当你再次睁开双眼，熟悉的疼痛却被陌生又熟悉的环境取代\n..............我重生了。";

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
    /// 破坏四维传送门胜利重生旁白
    /// </summary>
    public void DestroyPortalWinVoiceOver(Action action)
    {
        Show(destroyPortalWin, 8, action);
    }

    /// <summary>
    /// 破坏四维传送门失败重生旁白
    /// </summary>
    public void DestroyPortalLossVoiceOver(Action action)
    {
        Show(destroyPortalLoss, 8, action);
    }


    /// <summary>
    /// 空间坍缩重生旁白
    /// </summary>
    public void SpatialCollapse(Action action)
    {
        Show(testExplorerCollapseRebirth, 8, action);
    }


    /// <summary>
    /// 虫洞坍塌重生旁白
    /// </summary>
    public void WormholeCollapse(Action action)
    {
        Show(testExplorerCollapseRebirth, 8, action);
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