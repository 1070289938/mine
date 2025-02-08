using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源的全局加成
/// </summary>
public class ResourceAdditionManager : MonoBehaviour
{
    // Start is called before the first frame update
    ////////////////////////////////////////////////////资源类////////////////////////////////////////////////////////////////////////////
    double tool=1;//挖矿工具加成

    double miningWorker=1;//采矿工人加成


    double minerStone=1;//大锤采集加成（石头）


    double worker=1;//矿洞员工加成


    ////////////////////////////////////////////////////房屋类////////////////////////////////////////////////////////////////////////////

    double tenementComfort=1;//房屋坚固程度加成

    double tenementBasics = 1;//房屋基础加成

    double tenementRent = 1;//房屋房租加成

    double tenementSaveMoney=1;//房屋软妹币储量加成

    public static ResourceAdditionManager Instance { get; private set; }

    OreCarManager oreCarManager;//矿车管理




    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        oreCarManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.OreCar).GetComponent<OreCarManager>();//初始化矿车管理
    }

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
    /// <summary>
    /// 提升大锤加成（石头）
    /// </summary>
    /// <param name="count"></param>
    public void AddMinerStone(double count)
    {
        minerStone *= 1 + count;
    }
    /// <summary>
    /// 获取大锤加成（石头
    /// </summary>
    /// <returns></returns>
    public double GetMinerStoneUp()
    {

        double basics = 0; //基础加成是0
        basics += minerStone;   //大锤采集加成（石头
        return basics;
    }


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














}
