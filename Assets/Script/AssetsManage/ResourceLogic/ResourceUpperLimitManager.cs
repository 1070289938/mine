using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 资源上限管理
/// </summary>
public class ResourceUpperLimitManager : MonoBehaviour
{

    public static ResourceUpperLimitManager Instance { get; private set; }

    public StashManager stashManager;//仓库

    public IndustrialReserveStationManager industrialReserveStationManager;//工业储备站

    public TenementManager tenementManager;//房屋

    public BankManager bankManager;//房屋
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //刷新所有资源的上限
    public void RefreshUpperLimitAllResources()
    {
        UpperLimitRMB();//计算软妹币
        UpperLimitStone();//计算石矿
        UpperLimitCopper();//计算铜矿
        UpperLimitIron();//计算铁矿
        UpperLimitCement();//计算水泥
        UpperLimitColliery();//计算煤矿
        UpperLimitSteel();//计算钢
        UpperLimitSilicon();//计算硅矿
    }
    /// <summary>
    /// 计算软妹币上限
    /// 软妹币上限 = 房屋储量
    /// </summary>
    void UpperLimitRMB()
    {
        double reserves = 0; //基本储量0
        reserves += tenementManager.GetReserves();//获取房屋储量

        reserves += bankManager.GetReserves();//获取房屋储量


        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Currency];
        resourceShow.SetMaxStorage(reserves);//软妹币矿设置上限
    }


    /// <summary>
    /// 计算石矿上限
    /// 石矿上限 = 基本100 + 仓库储量
    /// </summary>
    void UpperLimitStone()
    {
        double reserves = 100; //基本储量100
        reserves += stashManager.GetReserves(ResourceType.Stone);//获取仓库储量
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Stone];

        resourceShow.SetMaxStorage(reserves);//石矿设置上限
    }


    /// <summary>
    /// 计算铜矿上限
    /// 铜矿上限 =  基本100 +仓库储量
    /// </summary>
    void UpperLimitCopper()
    {
        double reserves = 100; //基本储量100
        reserves += stashManager.GetReserves(ResourceType.Copper);//获取仓库储量
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Copper];
        resourceShow.SetMaxStorage(reserves);//铜矿设置上限
    }

    /// <summary>
    /// 计算铁矿上限
    /// 铜矿上限 =  基本100 +仓库储量
    /// </summary>
    void UpperLimitIron()
    {
        double reserves = 100; //基本储量100
        reserves += stashManager.GetReserves(ResourceType.Iron);//获取仓库储量
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Iron];
        resourceShow.SetMaxStorage(reserves);//铁矿设置上限
    }

    /// <summary>
    /// 计算水泥上限
    /// 铜矿上限 =  仓库储量
    /// </summary>
    void UpperLimitCement()
    {
        double reserves = 0; //基本储量100
        reserves += stashManager.GetReserves(ResourceType.Cement);//获取仓库储量
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Cement];
        resourceShow.SetMaxStorage(reserves);//水泥设置上限
    }




    /// <summary>
    /// 计算煤矿上限
    /// 煤矿上限 =  仓库储量
    /// </summary>
    void UpperLimitColliery()
    {
        double reserves = 0; //基本储量0
        reserves += stashManager.GetReserves(ResourceType.Colliery);//获取仓库储量
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Colliery];
        resourceShow.SetMaxStorage(reserves);//煤矿设置上限
    }




    /// <summary>
    /// 计算钢上限
    /// 钢上限 =  工业储备站储量
    /// </summary>
    void UpperLimitSteel()
    {
        double reserves = 50; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Steel);//获取仓库储量
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Steel];
        resourceShow.SetMaxStorage(reserves);//钢设置上限
    }

    /// <summary>
    /// 计算硅矿上限
    /// 硅矿上限 =  工业储备站储量
    /// </summary>
    void UpperLimitSilicon()
    {
        double reserves = 200; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Silicon);//获取仓库储量
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Silicon];
        resourceShow.SetMaxStorage(reserves);//硅设置上限
    }
}
