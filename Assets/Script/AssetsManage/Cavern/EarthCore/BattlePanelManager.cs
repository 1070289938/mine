using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 战斗面板
/// </summary>
public class BattlePanelManager : MonoBehaviour
{
    // 危险
    public TextMeshProUGUI dangerText;

    // 战斗力
    public TextMeshProUGUI combatPowerText;
    // 战斗力
    public TextMeshProUGUI attackCombatPowerText;
    // 士兵数
    public TextMeshProUGUI soldierCountText;
    // 出击士兵数
    public TextMeshProUGUI attackSoldierCountText;


    // 生产进度
    public Image schedule;

    // 增加按钮
    public Button addButton;

    // 减少按钮
    public Button decreaseButton;

    public Toggle toggle;
    // 多选框内容
    public Image toggleImage;
    int danger = 0; // 危险值(默认0)
    int maxDanger = 100000; // 最大危险值(默认10w)
    int combatPower = 0; // 驻地战斗力
    int soldierCap = 0; // 士兵上限
    int soldierCount = 0; // 士兵数量
    int attackPower = 0; // 出击战斗力
    int attackCount = 0; // 出击数量

    double outputSpeed = 30;
    //自动填充
    bool autofill = false;

    // 兵营
    public BarracksManager barracksManager;

    // 自动炮塔
    public AutomaticTurretManager automaticTurretManager;
    // 激光炮塔
    public LaserTurretManager laserTurretManager;
    // 探测队
    public FacilityPanelManager explorationTeamManager;

    public static BattlePanelManager Instance;
    /// <summary>
    /// 新兵训练营
    /// </summary>
    public BootCampManager bootCampManager;

    void OnToggleValueChanged(bool isOn)
    {
        autofill = isOn;
        toggleImage.gameObject.SetActive(isOn);

    }
    public VerticalLayoutGroup verticalLayoutGroup;

    public void Activate()
    {
        gameObject.SetActive(true);
        verticalLayoutGroup.padding.top = 250;
        if (!TechManager.Instance.GetTechFlag(TechType.GeocentricDog))
        {
            LogManager.Instance.AddWarnLog("有人在大门内发现了浑身长满尖刺外壳,双眼是血红色的犬类未知生物,被命名为地心犬,他会撕裂所有看到的生物十分危险!");
            TechManager.Instance.techTypeStudyFlag[TechType.GeocentricDog] = true;
        }

    }

    void Awake()
    {
        Instance = this;
        addButton.onClick.AddListener(AddAttackCount);
        decreaseButton.onClick.AddListener(DecreaseAttackCount);
        // 为每个Toggle添加监听事件
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 增加出击数量
    /// </summary>
    void AddAttackCount()
    {
        if (soldierCount > 0)
        {
            attackCount++;
            soldierCount--;
            attackSoldierCountText.text = attackCount.ToString();
            UpdateSoldierMax();
        }
    }

    /// <summary>
    /// 减少出击数量
    /// </summary>
    void DecreaseAttackCount()
    {
        if (attackCount > 0)
        {
            attackCount--;
            soldierCount++;
            attackSoldierCountText.text = attackCount.ToString();
            UpdateSoldierMax();
        }
    }

    // 可能遇到东西
    float[] chances = { 0.9f, 0.75f, 0.5f, 0.3f };
    string[] names = { "落单的地心犬", "地心犬小型集体", "成群的地心犬", "成群的嗜血地心犬" };

    /// <summary>
    /// 巡逻队遇到战斗
    /// </summary>
    void Sweep()
    {
        int id = Random.Range(0, 4);
        Log("巡逻队遭遇了" + names[id]);

        // 巡逻队有概率胜利
        if (Random.value < chances[id])
        {
            Win(id);
        }
        else
        {
            Lose(id);
        }
    }


    /// <summary>
    /// 驻地遭到了袭击
    /// </summary>
    void StationAttacked()
    {
        Log("驻地遭遇了地心犬的袭击!");
        // 计算胜率：驻地战斗力 / (驻地战斗力 + 危险值)
        float chance = (float)combatPower / Mathf.Max(combatPower + danger, 1);
        bool isWin = Random.value < chance;

        if (isWin)
        {
            // 驻地胜利的逻辑
            Log("驻地成功抵御了袭击！");

            // 随机死亡0 - 驻地兵力数量，0和驻地兵力数量/2以下的死亡数量的权重较大，0为最大
            int[] weights = new int[10];
            weights[0] = 0;
            for (int i = 1; i < 5; i++)
            {
                weights[i] = Random.Range(0, soldierCount / 2);
            }
            for (int i = 5; i < 10; i++)
            {
                weights[i] = Random.Range(0, soldierCount);
            }
            int randomDeath = GetWeightedRandomNumber(weights);
            LossSoldier(randomDeath);

            if (randomDeath > 0)
            {
                Log($"驻地在战斗中损失了 {randomDeath} 名士兵！");
            }
        }
        else
        {
            // 驻地失败的逻辑
            Log("驻地遭到袭击，防守失败！");

            // 随机扣除1 - 资源上限种类资源，随机扣除1% - 10%资源
            ResourceManager resourceManager = ResourceManager.Instance;
            //可用资源类型
            List<ResourceType> resourceTypes = new List<ResourceType>();
            foreach (var type in resourceManager.resourceUnlocks)
            {
                resourceTypes.Add(type.Key);
            }

            int randomResourceCount = Random.Range(1, resourceTypes.Count + 1);
            for (int i = 0; i < randomResourceCount; i++)
            {
                ResourceType randomResourceType = resourceTypes[Random.Range(0, resourceTypes.Count)];
                resourceTypes.Remove(randomResourceType);
                if (ResourceManager.Instance.special.ContainsKey(randomResourceType))
                {
                    continue;
                }
                double resourceAmount = ResourceManager.Instance.GetResource(randomResourceType);
                double lossPercentage = Random.Range(0.01f, 0.2f);
                double lossAmount = resourceAmount * lossPercentage;
                if (lossAmount != 0)
                {
                    ResourceManager.Instance.SpendResource(randomResourceType, lossAmount);
                    Log($"驻地损失了 {AssetsUtil.FormatNumber(lossAmount)} {randomResourceType.GetName()} ！");
                }

            }

            // 驻地的兵力随机死亡驻地兵力/2 - 驻地兵力数量
            int minLoss = (int)(soldierCount * 0.5);
            int lossCount = Random.Range(minLoss, soldierCount + 1);
            LossSoldier(lossCount);
            Log($"驻地损失了 {lossCount} 名士兵！");

        }
    }

    /// <summary>
    /// 损失士兵数量
    /// </summary>
    /// <param name="count"></param>
    void LossSoldier(int count)
    {
        soldierCount -= count;
        if (soldierCount < 0)
        {
            soldierCount = 0;
        }
        soldierCountText.text = soldierCount + "/" + soldierCap;
        UpdateSoldierMax();
    }






    /// <summary>
    /// 检查事件是否触发
    /// </summary>
    void CheckEventTrigger()
    {
        // 计算触发概率，最低 0.05%，最高 10%
        float minProbability = 0.0005f;
        float maxProbability = 0.1f;
        float probability = minProbability + (maxProbability - minProbability) * ((float)danger / maxDanger);

        if (Random.value < probability)
        {
            if (attackCount != 0)
            {
                Sweep();
            }
        }


    }

    /// <summary>
    /// 驻地袭击事件是否触发
    /// </summary>
    void EncampmentTrigger()
    {
        // 计算触发概率，最低 0.01%，最高 5%
        float minProbability = 0.0001f;
        float maxProbability = 0.05f;
        float probability = minProbability + (maxProbability - minProbability) * ((float)danger / maxDanger);

        if (Random.value < probability)
        {
            StationAttacked();
        }


    }


    /// <summary>
    /// 巡逻队胜利
    /// </summary>
    void Win(int id)
    {
        int[] weights = { 0, 0, 0, 0, 0,
            Random.Range(0, attackCount/2),
            Random.Range(0, attackCount/2),
            Random.Range(0, attackCount/2),
            Random.Range(0, attackCount/2),
            Random.Range(0, attackCount) }; // 这里的权重可以根据需要调整

        double reduction = 0;
        switch (id)
        {
            case 0:
                // 危险程度减少战斗力/10
                reduction = attackPower / 5;
                break;
            case 1:
                reduction = attackPower / 2;
                break;
            case 2:
                reduction = attackPower;
                break;
            case 3:
                reduction = attackPower * 1.5;
                break;
        }

        // 获取随机的死亡数
        int randomNumber = GetWeightedRandomNumber(weights);
        LossPatrol(randomNumber);

        LogBattleResult(id, randomNumber, true);
        RiskReduction(reduction);
    }

    /// <summary>
    /// 巡逻队失败
    /// </summary>
    void Lose(int id)
    {
        double reduction = 0;
        Debug.Log("id" + id);
        switch (id)
        {
            case 0:
                reduction = attackPower / 5 * 0.1;
                Debug.Log(reduction);
                break;
            case 1:
                reduction = attackPower / 2 * 0.1;
                Debug.Log(reduction);
                break;
            case 2:
                reduction = attackPower * 0.1;
                Debug.Log(reduction);
                break;
            case 3:
                reduction = attackPower * 0.15;
                Debug.Log(reduction);
                break;
        }

        int minLoss = (int)(attackCount * 0.5);
        int[] weights = new int[10];
        for (int i = 0; i < 10; i++)
        {
            weights[i] = Random.Range(minLoss, attackCount + 1);
        }
        int randomNumber = GetWeightedRandomNumber(weights);
        LossPatrol(randomNumber);

        LogBattleResult(id, randomNumber, false);
        RiskReduction(reduction);
    }
    //降低危险值
    void RiskReduction(double count)
    {

        if (danger - count >= 500)
        {
            Log("降低" + (int)count + "危险程度");
            danger -= (int)count;
        }
        else
        {
            danger = 500;
            Log("危险程度已经降至最低");
        }
    }

    /// <summary>
    /// 损失巡逻队
    /// </summary>
    /// <param name="count"></param>
    void LossPatrol(int count)
    {
        attackCount -= count;
        if (attackCount < 0)
        {
            attackCount = 0;
        }
        attackSoldierCountText.text = attackCount.ToString();
        UpdateSoldierMax();
    }

    // 根据权重获取随机数的方法
    int GetWeightedRandomNumber(int[] weights)
    {
        int totalWeight = 0;
        // 计算总权重
        foreach (int weight in weights)
        {
            totalWeight += weight;
        }

        // 生成一个 0 到总权重之间的随机数
        int randomValue = Random.Range(0, totalWeight);
        int cumulativeWeight = 0;

        // 遍历权重数组，根据累计权重判断随机数落在哪个区间
        for (int i = 0; i < weights.Length; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return weights[i];
            }
        }

        return weights[0];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    float time = 0;
    // 每秒运行事件的计时器
    float eventTimer = 0;

    // Update is called once per frame
    void Update()
    {
        Output();
        CombatEffectivenessCalculation();
        UpdateDanger();
        UpDanger();

        // 每秒运行事件
        eventTimer += Time.deltaTime;
        if (eventTimer >= 1)
        {
            eventTimer -= 1;
            CheckEventTrigger();
            EncampmentTrigger();
        }
    }


    /// <summary>
    /// 每秒进行实时更新危险程度
    /// </summary>
    void UpDanger()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            //UpdateMax(); // 更新计算最大危险程度
            time -= 1;
            double increase = maxDanger * 0.00003475;
            increase += danger;

            if (increase > maxDanger)
            {
                danger = maxDanger;
            }
            else
            {
                danger = (int)increase;
            }
        }
    }

    /// <summary>
    /// 实时更新危险程度
    /// </summary>
    void UpdateDanger()
    {
        dangerText.text = AssetsUtil.FormatNumber(danger);
    }

    /// <summary>
    /// 实时的对战斗力进行计算
    /// </summary>
    void CombatEffectivenessCalculation()
    {
        double basic = 5;
        basic *= ResourceAdditionManager.Instance.GetCombatPowerUp();

        // 驻地的战斗力
        double soldierCombatPower = soldierCount * basic;
        combatPower = (int)(soldierCombatPower + automaticTurretManager.GetUp() + laserTurretManager.GetUp());
        combatPowerText.text = AssetsUtil.FormatNumber(combatPower);

        // 出击战斗力
        double attackSoldierCombatPower = attackCount * basic;
        attackPower = (int)attackSoldierCombatPower;
        attackCombatPowerText.text = AssetsUtil.FormatNumber(attackSoldierCombatPower);
    }

    /// <summary>
    /// 更新士兵的上限
    /// </summary>
    public void UpdateSoldierMax()
    {
        int max = 0;
        max += barracksManager.GetCount();
        max -= attackCount;
        soldierCap = max;
        soldierCountText.text = soldierCount + "/" + soldierCap;
    }

    /// <summary>
    /// 增加士兵数量
    /// </summary>
    /// <param name="count"></param>
    void AddSoldier(int count)
    {

        soldierCount += count;
        soldierCountText.text = soldierCount + "/" + soldierCap;
        //如果自动填充开着就直接增加出击数量
        if (autofill)
        {
            for (int i = 0; i < count; i++)
            {
                AddAttackCount();
            }
        }
    }

    void Output()
    {
        // 如果数量小于上限就进行生产
        if (soldierCount < soldierCap)
        {
            double speed = Time.deltaTime / outputSpeed;

            speed *= bootCampManager.GetUp();

            schedule.fillAmount += (float)speed;
            if (schedule.fillAmount >= 1)
            {
                schedule.fillAmount = 0;
                AddSoldier(1);
            }
        }
    }

    public int GetSoldierCount()
    {
        return soldierCount;
    }

    public int GetattackCount()
    {
        return attackCount;
    }


    public int GetdangerCount()
    {
        return danger;
    }
    public bool GetAutofillCount()
    {
        return autofill;
    }


    /// <summary>
    /// 初始化
    /// </summary>
    public void Install(int soldierCount, int attackCount, int danger, bool autofill)
    {
        AddSoldier(soldierCount);
        this.attackCount = attackCount;
        attackSoldierCountText.text = attackCount.ToString();
        this.danger = danger;
        this.autofill = autofill;
        toggleImage.gameObject.SetActive(autofill);
        UpdateSoldierMax();
    }

    /// <summary>
    /// 更新最大危险值
    /// </summary>
    public void UpdateMax()
    {
        int danger = 0;

        // 探索队每个提升500危险值
        danger += explorationTeamManager.GetCount() * 500;

        maxDanger = danger;
    }

    /// <summary>
    /// 记录日志
    /// </summary>
    /// <param name="message"></param>
    void Log(string message)
    {
        LogManager.Instance.AddWarnLog(message);
    }
    /// <summary>
    /// 获取总战斗力
    /// </summary>
    /// <returns></returns>
    public int GetPower()
    {
        return combatPower + attackPower;
    }

    /// <summary>
    /// 记录战斗结果日志
    /// </summary>
    /// <param name="id"></param>
    /// <param name="randomNumber"></param>
    /// <param name="isWin"></param>
    void LogBattleResult(int id, int randomNumber, bool isWin)
    {
        if (isWin)
        {
            if (randomNumber == 0)
            {
                switch (id)
                {
                    case 0:
                        Log("巡逻队轻松的消灭了落单的地心犬");
                        break;
                    case 1:
                        Log("巡逻队使用突袭的战术迅速的剿灭了地心犬小型集体");
                        break;
                    case 2:
                        Log("巡逻队发挥超常,轻松的剿灭了成群的地心犬");
                        break;
                    case 3:
                        Log("巡逻队奇迹般的在毫无伤员的情况下险胜了成群的嗜血地心犬");
                        break;
                }
            }
            else if (randomNumber < attackCount / 2)
            {
                switch (id)
                {
                    case 0:
                        Log("巡逻队消灭了落单的地心犬,但是有少量的兄弟意外死亡");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 1:
                        Log("巡逻队消灭地心犬小型集体,但不少的兄弟搭进去了");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 2:
                        Log("巡逻队消灭了成群的地心犬!");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 3:
                        Log("巡逻队险胜成群的嗜血地心犬!");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                }
            }
            else if (randomNumber > attackCount / 2)
            {
                switch (id)
                {
                    case 0:
                        Log("巡逻队遇到了落单的的地心犬,但是因为大意而折损了大量士兵!");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 1:
                        Log("巡逻队遭到了地心犬小型集体的袭击,折损了大量兵力");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 2:
                        Log("巡逻队踏入了成群的地心犬的陷阱,损失了大量兵力");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 3:
                        Log("巡逻队不幸遇到了成群的嗜血地心犬,士兵们损失惨重");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                }
            }
        }
        else
        {
            if (randomNumber == attackCount)
            {
                switch (id)
                {
                    case 0:
                        Log("巡逻队被落单的地心犬全歼，无一生还！");
                        break;
                    case 1:
                        Log("巡逻队被地心犬小型集体打得全军覆没！");
                        break;
                    case 2:
                        Log("巡逻队遭遇成群的地心犬，惨遭灭顶之灾！");
                        break;
                    case 3:
                        Log("巡逻队遇到成群的嗜血地心犬，全军覆灭！");
                        break;
                }
            }
            else if (randomNumber >= attackCount * 0.75)
            {
                switch (id)
                {
                    case 0:
                        Log("巡逻队被落单的地心犬击败，损失惨重，仅少数士兵生还！");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 1:
                        Log("巡逻队不敌地心犬小型集体，大部分士兵牺牲！");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 2:
                        Log("巡逻队被成群的地心犬打败，损失超过四分之三的兵力！");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 3:
                        Log("巡逻队遇到成群的嗜血地心犬，死伤大半！");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                }
            }
            else if (randomNumber >= attackCount * 0.5)
            {
                switch (id)
                {
                    case 0:
                        Log("巡逻队被落单的地心犬击败，超过一半的士兵阵亡！");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 1:
                        Log("巡逻队不敌地心犬小型集体，一半以上的士兵牺牲！");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 2:
                        Log("巡逻队被成群的地心犬打败，损失超过一半的兵力！");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                    case 3:
                        Log("巡逻队遇到成群的嗜血地心犬，一半以上士兵死亡！");
                        Log("死亡" + randomNumber + "个士兵");
                        break;
                }
            }
        }
    }
}