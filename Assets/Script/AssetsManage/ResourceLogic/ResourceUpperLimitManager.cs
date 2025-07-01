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
    public DiscoveryTowerManager discoveryTowerManager;//科技探索塔

    public ExtraterrestrialMaterialLaboratoryManager extraterrestrialMaterialLaboratoryManager;//外星材料实验室
    public TenementManager tenementManager;//房屋

    public BankManager bankManager;//房屋

    public StockExchangeManager stockExchangeManager;//证券交易所

    public MemoryStorageManager memoryStorageManager;//记忆储存装置

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

    /// <summary>
    /// 刷新所有资源的上限
    /// </summary>
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
        UpperLimitAluminum();//计算铝矿
        UpperLimitTitanium();//计算钛矿
        UpperLimitAlloy();//计算合金
        UpperZorizun();//计算佐里旬矿上限
        UpperScience();//计算科技上限


        UpperSilver();//计算银矿上限
        UpperTungsten();//计算钨矿上限
        UpperNickel();//计算镍矿上限
        UpperNanomaterials();//计算纳米材料上限
        UpperGeocentricRock();//计算地心岩上限

        UpperIridium();//铱
        UpperMithril();//秘银
        UpperNeutron();//中子

        UpperAdamant();//金属氢
        UpperFlare();//耀斑矿
        UpperMetallicHydrogen();//精金


        UpperMudCrystal();//暮晶
        UpperMemoryAlloy();//记忆合金
    }
    /// <summary>
    /// 计算软妹币上限
    /// 软妹币上限 = 房屋储量
    /// </summary>
    void UpperLimitRMB()
    {
        double reserves = 0; //基本储量0
        reserves += tenementManager.GetReserves();//获取房屋储量

        reserves += bankManager.GetReserves();//获取银行储量

        reserves *= stockExchangeManager.GetReservesUp();//证券交易所对软妹币的储量提升


        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成


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

        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成

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

        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成

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

        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成

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

        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成

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

        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成

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
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Steel);//获取工业储备站储量

        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成


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
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Silicon);//获取工业储备站储量

        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成

        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Silicon];
        resourceShow.SetMaxStorage(reserves);//硅设置上限
    }


    /// <summary>
    /// 计算铝矿上限
    /// 铝矿上限 =  工业储备站储量
    /// </summary>
    void UpperLimitAluminum()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Aluminum);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Aluminum];
        resourceShow.SetMaxStorage(reserves);//铝设置上限
    }

    /// <summary>
    /// 计算钛矿上限
    /// 钛矿上限 =  工业储备站储量
    /// </summary>
    void UpperLimitTitanium()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Titanium);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Titanium];
        resourceShow.SetMaxStorage(reserves);//钛设置上限
    }


    /// <summary>
    /// 计算合金矿上限
    /// 合金矿上限 =  工业储备站储量
    /// </summary>
    void UpperLimitAlloy()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Alloy);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Alloy];
        resourceShow.SetMaxStorage(reserves);//合金设置上限
    }

    /// <summary>
    /// 计算佐里旬矿上限
    /// 佐里旬矿上限 =  工业储备站储量
    /// </summary>
    void UpperZorizun()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Zorizun);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Zorizun];
        resourceShow.SetMaxStorage(reserves);//佐里旬设置上限
    }

    /// <summary>
    /// 计算科技点上限
    /// 科技上限 =  科技探索塔储量
    /// </summary>
    void UpperScience()
    {
        double reserves = 100; //基本储量0
        reserves += discoveryTowerManager.GetReserves(ResourceType.Science);//获取科技探索塔储量
        reserves += extraterrestrialMaterialLaboratoryManager.GetReserves(ResourceType.Science);//获取科技探索塔储量
        //科技不受储量加成
        //reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成

        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Science];
        resourceShow.SetMaxStorage(reserves);//科技设置上限
    }




    /// <summary>
    /// 计算银矿上限
    /// 银矿上限 =  仓库储量
    /// </summary>
    void UpperSilver()
    {
        double reserves = 100; //基本储量0
        reserves += stashManager.GetReserves(ResourceType.silver);//获取科技探索塔储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.silver];
        resourceShow.SetMaxStorage(reserves);//银矿设置上限
    }




    /// <summary>
    /// 计算钨矿上限
    /// 钨矿上限 =  工业储备站储量
    /// </summary>
    void UpperTungsten()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Tungsten);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Tungsten];
        resourceShow.SetMaxStorage(reserves);//钨设置上限
    }

    /// <summary>
    /// 计算镍矿上限
    /// 镍矿上限 =  工业储备站储量
    /// </summary>
    void UpperNickel()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Nickel);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Nickel];
        resourceShow.SetMaxStorage(reserves);//镍设置上限
    }

    /// <summary>
    /// 计算纳米材料上限
    /// 纳米材料上限 =  工业储备站储量
    /// </summary>
    void UpperNanomaterials()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Nanomaterials);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Nanomaterials];
        resourceShow.SetMaxStorage(reserves);//纳米材料设置上限
    }

    /// <summary>
    /// 计算地心岩上限
    /// 地心岩上限 =  仓库储量
    /// </summary>
    void UpperGeocentricRock()
    {
        double reserves = 100; //基本储量0
        reserves += stashManager.GetReserves(ResourceType.GeocentricRock);//获取仓库储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.GeocentricRock];
        resourceShow.SetMaxStorage(reserves);//地心岩设置上限
    }

    /// <summary>
    /// 计算铱矿上限
    /// 铱矿上限 =  仓库储量
    /// </summary>
    void UpperIridium()
    {
        double reserves = 100; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Iridium);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Iridium];
        resourceShow.SetMaxStorage(reserves);//铱矿上限
    }

    /// <summary>
    /// 计算秘银上限
    /// 秘银上限 =  仓库储量
    /// </summary>
    void UpperMithril()
    {
        double reserves = 100; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Mithril);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Mithril];
        resourceShow.SetMaxStorage(reserves);//秘银设置上限
    }

    /// <summary>
    /// 计算中子上限
    /// 秘银上限 =  仓库储量
    /// </summary>
    void UpperNeutron()
    {
        double reserves = 100; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Neutron);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Neutron];
        resourceShow.SetMaxStorage(reserves);//中子设置上限
    }
    /// <summary>
    /// 计算金属氢上限
    /// 秘银上限 =  仓库储量
    /// </summary>
    void UpperMetallicHydrogen()
    {
        double reserves = 0; //基本储量0
        reserves += stashManager.GetReserves(ResourceType.MetallicHydrogen);//获取仓库储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.MetallicHydrogen];
        resourceShow.SetMaxStorage(reserves);//金属氢设置上限
    }

    /// <summary>
    /// 计算耀斑矿上限
    /// 秘银上限 =  仓库储量
    /// </summary>
    void UpperFlare()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Flare);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Flare];
        resourceShow.SetMaxStorage(reserves);//耀斑矿设置上限
    }


    /// <summary>
    /// 计算精金上限
    /// 秘银上限 =  仓库储量
    /// </summary>
    void UpperAdamant()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.Adamant);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.Adamant];
        resourceShow.SetMaxStorage(reserves);//精金设置上限
    }

    /// <summary>
    /// 计算暮晶上限
    /// 秘银上限 =  仓库储量
    /// </summary>
    void UpperMudCrystal()
    {
        double reserves = 0; //基本储量0
        reserves += industrialReserveStationManager.GetReserves(ResourceType.MudCrystal);//获取工业储备站储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.MudCrystal];
        resourceShow.SetMaxStorage(reserves);//暮晶设置上限
    }

    /// <summary>
    /// 计算记忆合金上限
    /// 秘银上限 =  仓库储量
    /// </summary>
    void UpperMemoryAlloy()
    {
        double reserves = 500; //基本储量0
        reserves += memoryStorageManager.GetReserves(ResourceType.memoryAlloy);//获取记忆储存装置储量
        reserves *= ResourceAdditionManager.Instance.GetAllReservesUp();//获取所有的储量加成
        ResourceShowManager resourceShow = ResourceManager.Instance.resourceManager[ResourceType.memoryAlloy];
        resourceShow.SetMaxStorage(reserves);//暮晶设置上限
    }




}
