using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 定义事件类
public class Incident
{
    public string description;
    public float probabilityWeight;
    public Action action;
    public TechType prerequisiteTech; // 前置科技
    public TechType mainTech; // 主线科技

    // 用于区域事件的构造方法
    public Incident(string description, float probabilityWeight, Action action = null)
    {
        this.description = description;
        this.probabilityWeight = probabilityWeight;
        this.action = action;
    }

    // 用于主线事件的构造方法
    public Incident(string description, float probabilityWeight, TechType prerequisiteTech, TechType mainTech, Action action = null)
    {
        this.description = description;
        this.probabilityWeight = probabilityWeight;
        this.action = action;
        this.prerequisiteTech = prerequisiteTech;
        this.mainTech = mainTech;
    }
}

public class IncidentManager : MonoBehaviour
{
    // 存储每个区域的事件列表
    private Dictionary<MapType, List<Incident>> regionIncidents = new Dictionary<MapType, List<Incident>>();
    // 主线事件列表
    private List<Incident> mainIncidents = new List<Incident>();

    // 区域事件触发总概率
    public float regionProbability = 0.01f;
    // 主线事件触发总概率
    public float mainProbability = 0.005f;

    public MapManager mapManager;

    void Awake()
    {
        // 初始化每个区域的事件列表
        InitializeRegionIncidents();
        // 初始化主线事件列表
        InitializeMainIncidents();
    }

    void Start()
    {
        // 启动当前区域的事件协程
        StartCoroutine(ExecuteCurrentRegionEvents());
        // 启动主线事件协程
        StartCoroutine(ExecuteMainEvents());
    }

    private void InitializeRegionIncidents()
    {
        // 矿洞事件
        regionIncidents[MapType.Mining] = new List<Incident>
        {
            new Incident("你发现有一只蟑螂从你的脚边爬了过去", 1f),
            new Incident("你感觉背后凉凉的,原来是有一滴水滴到了你的后脑勺", 1f),
            new Incident("你被一颗石子绊倒了", 1f),
            new Incident("你看到地上有一条蚂蚁排成的队伍", 1f),
            new Incident("你偷偷在无人的角落上了个厕所....", 1f),
            new Incident("你不小心踩死了一只蟑螂..蟑螂的酱汁在鞋底难以擦除", 1f),
            new Incident("你有点困了..", 1f),
            new Incident("疼!你被不知道哪里弹来的石子砸到了脑袋", 1f),
            new Incident("有两个矿工打起来了!", 1f)
        };

        // 矿场事件
        regionIncidents[MapType.MineField] = new List<Incident>
        {
            new Incident("一颗流星从天而降。", 1f),
            new Incident("一场漂亮的流星雨吸引了大众的目光。", 1f),
            new Incident("您捡到了一些软妹币，今天运气不错。", 1f),
            new Incident("你注意到一朵云，看起来就像是口喷唾沫的美洲驼。", 1f),
            new Incident("你注意到一朵云，看起来就像是河豚。", 1f),
            new Incident("你注意到一朵云，看起来就像是……就是一朵云。", 1f),
            new Incident("你注意到一朵云，看起来就像是在枕头大战的床垫。", 1f),
            new Incident("你注意到一朵云，看起来就像是一对老夫妇，安安静静坐着等待生命终结。", 1f),
            new Incident("你注意到一朵云，看起来就像是父亲对你说，你让他很失望。", 1f),
            new Incident("你注意到一朵云，看起来就像是一头龙。", 1f),
            new Incident("你注意到一朵云，看起来就像是脏辫老头儿。", 1f),
            new Incident("你注意到一朵云，看起来就像是……想不出像什么。", 1f),
            new Incident("你注意到一朵云，看起来就像是朝下的大拇指。", 1f),
            new Incident("你注意到一朵云，看起来就像是不可描述之物。", 1f),
            new Incident("一只黑猫在您面前跑过。", 1f),
            new Incident("有朵云看起来像是一个巨型X，这是一个不祥之兆", 1f)
        };

        // 矿山事件
        regionIncidents[MapType.MineHill] = new List<Incident>
        {
            new Incident("山体突然有小块石头滑落", 1f),
            new Incident("你听到远处传来雪崩的声音", 1f),
            new Incident("一只老鹰在头顶盘旋", 1f),
            new Incident("你因为走神撞到树上了", 1f),
            new Incident("一只老鹰在头顶盘旋,并且在你头上拉了一坨屎", 1f),
            new Incident("你发现了一个隐藏的山洞入口", 1f),
            new Incident("突然一阵强风刮过，差点把你吹倒", 1f),
            new Incident("你看到一只野兔从草丛中窜过", 1f),
            new Incident("远处传来狼的嚎叫，让你不寒而栗", 1f),
            new Incident("你脚下的土地突然塌陷了一小块", 1f),
            new Incident("树枝上一只松鼠好奇地盯着你", 1f),
            new Incident("一场小雪开始飘落，周围变得银装素裹", 1f),
            new Incident("你闻到了一股淡淡的花香，附近可能有花丛", 1f),
            new Incident("一块松动的岩石从山坡上滚落下来", 1f),
            new Incident("一只猫头鹰在树梢上发出诡异的叫声", 1f)
        };

        // 地核事件
        regionIncidents[MapType.EarthCore] = new List<Incident>
        {
            new Incident("周围的岩浆突然剧烈翻滚", 1f),
            new Incident("你感受到一股强大的热浪袭来", 1f),
            new Incident("有一块岩浆溅到了你的防护装备上", 1f),
            new Incident("你听到岩浆中传来奇怪的轰鸣声", 1f),
            new Incident("防护装备发出警报，提示温度过高", 1f),
            new Incident("你发现岩浆中似乎有某种生物在游动", 1f),
            new Incident("岩浆表面突然出现了一个巨大的气泡并破裂", 1f),
            new Incident("一股刺鼻的硫磺气味弥漫开来", 1f),
            new Incident("你的防护装备出现了轻微的裂缝", 1f),
            new Incident("周围的岩浆流动方向突然改变", 1f),
            new Incident("你感觉到脚下的地面在轻微震动", 1f),
            new Incident("一道炽热的岩浆柱从地下喷射而出", 1f),
            new Incident("岩浆中闪烁着奇异的光芒，像是蕴含着神秘力量", 1f),
            new Incident("你听到了岩浆内部传来的低沉咆哮声", 1f)
        };

        // 太空事件
        regionIncidents[MapType.OuterSpace] = new List<Incident>
        {
            new Incident("你的飞船遇到了小型陨石撞击", 1f),
            new Incident("你发现远处有神秘的能量波动", 1f),
            new Incident("飞船的导航系统出现了短暂故障", 1f),
            new Incident("飞船的生命维持系统发出轻微警报", 1f),
            new Incident("你看到一颗绚丽的星云从飞船旁飘过", 1f),
            new Incident("飞船与星球的通讯信号出现干扰", 1f),
            new Incident("飞船的太阳能板被一颗微小的流星击中", 1f),
            new Incident("你检测到附近有一股未知的辐射源", 1f),
            new Incident("飞船的引擎发出异常的噪音", 1f),
            new Incident("你看到一个神秘的黑色物体在太空中漂浮", 1f),
            new Incident("飞船的仪表盘上出现了一个不明符号", 1f),
            new Incident("一阵强大的宇宙射线掠过飞船", 1f),
            new Incident("你发现飞船的氧气储备略有下降", 1f),
            new Incident("飞船的外壳出现了一处轻微的划痕", 1f)
        };

        // 外星域事件
        regionIncidents[MapType.Extraterrestrial] = new List<Incident>
        {
            new Incident("一群外形奇特的生物从你身边飞过", 1f),
            new Incident("你闻到了一股刺鼻的气味", 1f),
            new Incident("地上出现了奇怪的符号", 1f),
            new Incident("你感觉到有一双眼睛在暗中注视着你", 1f),
            new Incident("突然一阵奇异的光芒闪过，周围的景象变得模糊", 1f),
            new Incident("你听到一种从未听过的声音，像是某种生物的叫声", 1f),
            new Incident("一片奇异的雾气弥漫开来，让你视线受阻", 1f),
            new Incident("地面突然开始震动，像是有巨大的物体在移动", 1f),
            new Incident("你发现周围的植物都发出诡异的光芒", 1f),
            new Incident("一只巨大的外星昆虫从洞穴中爬了出来", 1f),
            new Incident("空气中弥漫着一种粘稠的物质", 1f),
            new Incident("你看到远处有一座闪烁着光芒的建筑", 1f),
            new Incident("一个神秘的能量球在半空中漂浮", 1f),
            new Incident("你的通讯设备突然收到一串乱码信息", 1f)
        };

        // 异世界事件
        regionIncidents[MapType.AnotherWorld] = new List<Incident>
        {
            new Incident("天空中突然出现了一道神秘的彩虹桥", 1f),
            new Incident("你听到了悠扬的魔法咒语声", 1f),
            new Incident("一只小精灵飞到了你的肩膀上", 1f),
            new Incident("地面突然冒出一朵散发着光芒的花朵", 1f),
            new Incident("你看到远处有一座漂浮的城堡", 1f),
            new Incident("一阵微风拂过，带来了一股甜美的香气", 1f),
            new Incident("一群独角兽在草地上奔跑嬉戏", 1f),
            new Incident("你遇到了一个会说话的树精灵", 1f),
            new Incident("一道神秘的魔法光芒照亮了周围的区域", 1f),
            new Incident("你发现脚下的土地变成了柔软的云朵", 1f),
            new Incident("一只巨龙在天空中盘旋飞过", 1f),
            new Incident("周围的石头突然变成了可爱的小动物", 1f),
            new Incident("你听到了远处传来的美妙音乐声", 1f),
            new Incident("一个神秘的魔法阵出现在你面前", 1f)
        };
    }

    private void InitializeMainIncidents()
    {
        // 主线事件 - 金属棒事件
        mainIncidents.Add(new Incident(null, 1f, TechType.MetalDetector, TechType.MetalBarFound, () =>
        {
            LogManager.Instance.AddLog("金属探测器探测到了一个奇怪的东西，貌似是一个金属棒");
            TechManager.Instance.techTypeStudyFlag[TechType.MetalBarFound] = true;
        }));


        // 主线事件 - 探索矿山事件
        mainIncidents.Add(new Incident(null, 1f, TechType.Train, TechType.DiscoveredMine, () =>
        {
            LogManager.Instance.AddLog("你在高处发现远处有一条庞大的山脉,并想去看看");
            TechManager.Instance.techTypeStudyFlag[TechType.DiscoveredMine] = true;
        }));


        // 主线事件 - 发现生活痕迹事件
        mainIncidents.Add(new Incident(null, 1f, TechType.SilverMiner, TechType.FindTrace, () =>
        {
            LogManager.Instance.AddLog("银矿工人们在挖掘银矿的时候发现这附近貌似有人类生活的痕迹");
            TechManager.Instance.techTypeStudyFlag[TechType.FindTrace] = true;
        }));


        // 主线事件 - 发现立碑中所说的大门事件
        mainIncidents.Add(new Incident(null, 1f, TechType.AnalyticLanguage, TechType.DiscoverGate, () =>
        {
            LogManager.Instance.AddLog("有人汇报说是发现了立碑中所说的大门");
            TechManager.Instance.techTypeStudyFlag[TechType.DiscoverGate] = true;

        }));

        // 主线事件 - 发现地心岩事件
        mainIncidents.Add(new Incident(null, 1f, TechType.ExplorationTeam, TechType.Geocentric, () =>
        {
            LogManager.Instance.AddLog("探测队在探测的时候发现了一种只有地心存在的特殊岩石,他可以完全隔离地心的高温,并且形状泛红,科学家们给他命名叫做地心岩");
            TechManager.Instance.techTypeStudyFlag[TechType.Geocentric] = true;
        }));
      
    }

    // 执行当前区域事件
    private IEnumerator ExecuteCurrentRegionEvents()
    {
        while (true)
        {
            MapType currentRegion = GetCurrentMapType();
            Debug.Log($"执行 {currentRegion} 区域的协程");
            float randomValue = UnityEngine.Random.value;
            if (randomValue < regionProbability)
            {
                Carry(regionIncidents[currentRegion]);
            }
            yield return new WaitForSeconds(1f);
        }
    }

    // 执行主线事件
    private IEnumerator ExecuteMainEvents()
    {
        while (true)
        {

            float randomValue = UnityEngine.Random.value;
            if (randomValue < mainProbability)
            {
                foreach (var incident in mainIncidents)
                {
                    if (TechManager.Instance.GetTechFlag(incident.prerequisiteTech) && !TechManager.Instance.GetTechFlag(incident.mainTech))
                    {
                        Carry(new List<Incident> { incident });
                    }
                }
            }
            yield return new WaitForSeconds(1f);
        }
    }

    // 执行随机事件
    private void Carry(List<Incident> incidents)
    {

        // 计算总概率权重
        float totalWeight = 0f;
        foreach (var incident in incidents)
        {
            totalWeight += incident.probabilityWeight;
        }

        // 生成一个介于 0 到总概率权重之间的随机数
        float randomValue = UnityEngine.Random.Range(0f, totalWeight);
        float currentWeight = 0f;

        // 根据随机数选择事件
        foreach (var incident in incidents)
        {
            currentWeight += incident.probabilityWeight;
            if (randomValue < currentWeight)
            {
                if (incident.description != null)
                {
                    LogManager.Instance.AddLog(incident.description);
                }

                if (incident.action != null)
                {
                    incident.action.Invoke();
                }
                break;
            }
        }
    }

    // 获取当前区域枚举的方法
    private MapType GetCurrentMapType()
    {
        return mapManager.map;
    }
}