
using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 战斗面板
/// </summary>
public class BattlePanelManager : MonoBehaviour
{

    //危险
    public TextMeshProUGUI dangerText;


    //战斗力
    public TextMeshProUGUI combatPowerText;
    //战斗力
    public TextMeshProUGUI attackCombatPowerText;
    //士兵数
    public TextMeshProUGUI soldierCountText;
    //出击士兵数
    public TextMeshProUGUI attackSoldierCountText;

    //生产进度
    public Image schedule;

    //增加按钮
    public Button addButton;

    //减少按钮
    public Button decreaseButton;

    //扫荡
    public Button sweepButton;

    int danger = 100000;//危险值(默认10w)

    int maxDanger = 100000;//最大危险值(默认10w)

    int combatPower = 0;//驻地战斗力

    int soldierCap = 0;//士兵上限

    int soldierCount = 0;//士兵数量

    int attackPower = 0;//出击战斗力

    int attackCount = 0;//出击数量





    double outputSpeed = 30;
    //兵营
    public BarracksManager barracksManager;

    //自动炮塔
    public AutomaticTurretManager automaticTurretManager;
    //激光炮塔
    public LaserTurretManager laserTurretManager;


    public static BattlePanelManager Instance;

    void Awake()
    {
        Instance = this;
        addButton.onClick.AddListener(AddAttackCount);
        decreaseButton.onClick.AddListener(DecreaseAttackCount);
        sweepButton.onClick.AddListener(Sweep);
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


    /// <summary>
    /// 扫荡
    /// </summary>
    void Sweep()
    {

     

    }

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Output();
        CombatEffectivenessCalculation();
        UpdateDanger();
        UpDanger();


    }


    float time = 0;
    /// <summary>
    /// 每秒进行实时更新危险程度
    /// </summary>
    void UpDanger()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            time -= 1;
            double increase = maxDanger * 0.0556;
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

        //驻地的战斗力
        double soldierCombatPower = soldierCount * basic;
        combatPower = (int)(soldierCombatPower + automaticTurretManager.GetUp() + laserTurretManager.GetUp());
        combatPowerText.text = AssetsUtil.FormatNumber(combatPower);

        //出击战斗力
        double attackSoldierCombatPower = attackCount * basic;
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
    }

    void Output()
    {
        //如果数量小于上限就进行生产
        if (soldierCount < soldierCap)
        {
            double speed = Time.deltaTime / outputSpeed;
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


    /// <summary>
    /// 初始化
    /// </summary>
    public void Install(int soldierCount,int attackCount)
    {
        AddSoldier(soldierCount);

        this.attackCount = attackCount;
        attackSoldierCountText.text = attackCount.ToString();
        UpdateSoldierMax();
    }

}
