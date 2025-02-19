using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

/// <summary>
/// 资源的全局加成
/// </summary>
public class ResourceAdditionManager : MonoBehaviour
{
    ////////////////////////////////////////////////////资源类////////////////////////////////////////////////////////////////////////////
    double tool = 1;//挖矿工具加成
    double miningWorker = 1;//采矿工人加成
    double worker = 1;//矿洞员工加成
    double MineBonus = 1;//矿车的额外加成

    ////////////////////////////////////////////////////专项加成////////////////////////////////////////////////////////////////////////////
    double minerStone = 1;//石矿专项加成
    double CopperMine = 1;//铜矿专项加成
    double IronMine = 1;//铁矿专项加成
    double cement = 1;//水泥的专项加成
    double steel = 1;//钢的专项加成

    ////////////////////////////////////////////////////房屋类////////////////////////////////////////////////////////////////////////////
    double tenementComfort = 1;//房屋坚固程度加成
    double tenementBasics = 1;//房屋基础加成
    double tenementRent = 1;//房屋房租加成
    double tenementSaveMoney = 1;//房屋软妹币储量加成
    double container = 1;//集装箱加成
    double RMBboost = 1;//软妹币总产量
    double excavator = 1;//挖掘机提供的加成的加成
    double bankReserveSurcharge = 1;//银行储量加成

    public OreCarManager oreCarManager;//矿车管理
    public MineralScreeningMachineManager mineralScreeningMachineManager;//矿物筛选器
    public BlastFurnaceManager blastFurnaceManager;//高炉
    public ContainerManager containerManager;//集装箱加成
    public BankManager bankManager;//银行

    public static ResourceAdditionManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 初始化所有加成数据
    /// </summary>
    public void Initialize()
    {
        // 获取当前类的所有字段
        FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
        foreach (FieldInfo field in fields)
        {
            // 只处理 double 类型的字段
            if (field.FieldType == typeof(double))
            {
                field.SetValue(this, 1.0);
            }
        }
    }

    // 以下是原有的各种提升和获取加成的方法，保持不变
    /// <summary>
    /// 提升挖矿工具加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTool(double count)
    {
        tool *= 1 + count;
    }

    /// <summary>
    /// 获取挖矿工具加成
    /// </summary>
    public double GetToolUp()
    {
        double basics = 0; //基础加成是0
        basics += tool;
        return basics;
    }

    /// <summary>
    /// 提升采矿工人加成
    /// </summary>
    public void AddMiningWorker(double count)
    {
        miningWorker *= 1 + count;
    }
    /// <summary>
    /// 获取采矿工人加成
    /// </summary>
    /// <returns></returns>
    public double GetMiningWorkerUp()
    {
        double basics = 0; //基础加成是0
        basics += miningWorker;   //采矿工人基础加成
        basics += oreCarManager.GetOreCarUp();//矿车提升的采矿工人加成
        return basics;
    }
    //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓专项加成↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
    /// <summary>
    /// 提升石头专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddMinerStone(double count)
    {
        minerStone *= 1 + count;
    }
    /// <summary>
    /// 获取石头专项加成
    /// </summary>
    /// <returns></returns>
    public double GetMinerStoneUp()
    {
        double basics = 0; //基础加成是0
        basics += minerStone;   //大锤采集加成（石头
        return basics;
    }


    /// <summary>
    /// 提升铜矿专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddMinerCopper(double count)
    {
        CopperMine *= 1 + count;
    }
    /// <summary>
    /// 获取铜矿专项加成
    /// </summary>
    /// <returns></returns>
    public double GetMineCopperUp()
    {
        double basics = 0; //基础加成是0
        basics += CopperMine;   //铜矿专项加成
        basics += mineralScreeningMachineManager.GetscreenUp();//矿物筛选器加成
        return basics;
    }

    /// <summary>
    /// 提升铁矿专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddIronMine(double count)
    {
        IronMine *= 1 + count;
    }
    /// <summary>
    /// 获取铁矿专项加成
    /// </summary>
    /// <returns></returns>
    public double GetIronMineUp()
    {
        double basics = 0; //基础加成是0
        basics += IronMine;   //铁矿专项加成
        basics += mineralScreeningMachineManager.GetscreenUp();//矿物筛选器加成
        basics += blastFurnaceManager.GetBlastFurnaceUp();//高炉加成

        return basics;
    }

    /// <summary>
    /// 提升水泥专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddCement(double count)
    {
        cement *= 1 + count;
    }
    /// <summary>
    /// 获取水泥专项加成
    /// </summary>
    /// <returns></returns>
    public double GetCementUp()
    {
        double basics = 0; //基础加成是0
        basics += cement;   //水泥专项加成

        return basics;
    }


    /// <summary>
    /// 提升钢专项加成
    /// </summary>
    /// <param name="count"></param>
    public void AddSteel(double count)
    {
        steel *= 1 + count;
    }
    /// <summary>
    /// 获取钢专项加成
    /// </summary>
    /// <returns></returns>
    public double GetSteelUp()
    {
        double basics = 0; //基础加成是0
        basics += steel;   //钢专项加成

        return basics;
    }


    //↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑专项加成↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

    //↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓仓储加成↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓


    /// <summary>
    /// 获取仓库储量加成
    /// </summary>
    /// <returns></returns>
    public double GetStashUp()
    {
        double basics = 1; //基础加成是0
        basics += containerManager.GetUp();   //集装箱加成
        return basics;
    }



    /// <summary>
    /// 提升房屋软妹币储量加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTenementSaveMoney(double count)
    {
        tenementSaveMoney *= 1 + count;
    }
    /// <summary>
    /// 获取房屋软妹币储量加成
    /// </summary>
    /// <returns></returns>
    public double GetTenementSaveMoneyUp()
    {
        double basics = 0; //基础加成是0
        basics += tenementSaveMoney;   //矿洞员工加成
        return basics;
    }
    //↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑仓储加成↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑





    /// <summary>
    /// 提升房屋坚固程度加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTenementComfort(double count)
    {
        tenementComfort *= 1 + count;
    }
    /// <summary>
    /// 获取房屋坚固程度
    /// </summary>
    /// <returns></returns>
    public double GetTenementComfortUp()
    {
        double basics = 0; //基础加成是0
        basics += tenementComfort;   //房屋坚固程度加成
        return basics;
    }

    /// <summary>
    /// 提升房屋基础加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTenementBasics(double count)
    {
        tenementBasics *= 1 + count;
    }
    /// <summary>
    /// 获取房屋基础加成
    /// </summary>
    /// <returns></returns>
    public double GetTenementBasicsUp()
    {
        double basics = 0; //基础加成是0
        basics += tenementBasics;   //房屋坚固程度加成
        return basics;
    }


    /// <summary>
    /// 提升房屋房租加成
    /// </summary>
    /// <param name="count"></param>
    public void AddTenementRent(double count)
    {
        tenementRent *= 1 + count;
    }
    /// <summary>
    /// 获取房屋房租加成
    /// </summary>
    /// <returns></returns>
    public double GetTenementRentUp()
    {
        double basics = 0; //基础加成是0
        basics += tenementRent;   //房屋坚固程度加成
        return basics;
    }


    /// <summary>
    /// 提升矿洞员工加成
    /// </summary>
    /// <param name="count"></param>
    public void AddWorker(double count)
    {
        worker *= 1 + count;
    }
    /// <summary>
    /// 获取矿洞员工加成
    /// </summary>
    /// <returns></returns>
    public double GetWorkerUp()
    {
        double basics = 0; //基础加成是0
        basics += worker;   //矿洞员工加成
        return basics;
    }
    /// <summary>
    /// 提升矿车加成的加成
    /// </summary>
    /// <param name="count"></param>
    public void AddMineBonus(double count)
    {
        MineBonus *= 1 + count;
    }
    /// <summary>
    /// 获取矿车加成的加成
    /// </summary>
    /// <returns></returns>
    public double GetMineBonusUp()
    {
        double basics = 0; //基础加成是0
        basics += MineBonus;   //矿车加成
        return basics;
    }





    /// <summary>
    /// 提升软妹币加成
    /// </summary>
    /// <param name="count"></param>
    public void AddRMBboost(double count)
    {
        RMBboost *= 1 + count;
    }
    /// <summary>
    /// 获取软妹币加成
    /// </summary>
    /// <returns></returns>
    public double GetRMBboostUp()
    {
        double basics = 0; //基础加成是0
        basics += RMBboost;   //软妹币加成

        basics += bankManager.GetUp();


        return basics;
    }

    /// <summary>
    /// 提升集装箱加成
    /// </summary>
    /// <param name="count"></param>
    public void AddContainer(double count)
    {
        container *= 1 + count;
    }
    /// <summary>
    /// 获取集装箱加成
    /// </summary>
    /// <returns></returns>
    public double GetContainerUp()
    {
        double basics = 0; //基础加成是0
        basics += container;   //集装箱加成
        return basics;
    }






    /// <summary>
    /// 提升挖掘机加成
    /// </summary>
    /// <param name="count"></param>
    public void AddExcavator(double count)
    {
        excavator *= 1 + count;
    }
    /// <summary>
    /// 获取挖掘机加成
    /// </summary>
    /// <returns></returns>
    public double GetExcavatorUp()
    {
        double basics = 0; //基础加成是0
        basics += excavator;   //挖掘机加成
        return basics;
    }




    /// <summary>
    /// 提升银行储量加成
    /// </summary>
    /// <param name="count"></param>
    public void AddBankReserveSurcharge(double count)
    {
        bankReserveSurcharge *= 1 + count;
    }
    /// <summary>
    /// 获取银行储量加成
    /// </summary>
    /// <returns></returns>
    public double GetBankReserveSurchargeUp()
    {
        double basics = 0; //基础加成是0
        basics += bankReserveSurcharge;   //银行储量加成
        return basics;
    }
}