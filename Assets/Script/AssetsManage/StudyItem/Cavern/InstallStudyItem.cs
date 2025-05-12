using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallStudyItem : MonoBehaviour
{
    public int stage;//阶段
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 初始化科技
    /// </summary>
    public void Install()
    {

        foreach (TechnologyBean tech in DataProcessing.technologies)
        {
            //如果科技是当前阶段的科技，就显示出来
            if (tech.stage == stage)
            {
                GameObject instance = Instantiate(prefab, transform);
                StudyItemManager studyItem = instance.GetComponent<StudyItemManager>();
                InstallStudy(studyItem, tech);

            }
        }
        Debug.Log("--------第" + stage + "阶段科技生成完成--------");

    }

    void InstallStudy(StudyItemManager studyItem, TechnologyBean tech)
    {
        //研究成功了的说明
        studyItem.Successful = tech.successful;
        //点击事件
        studyItem.btn.onClick.AddListener(() =>
        {
            studyItem.ShowItemMsg();
        });
        //初始化框
        studyItem.Install(tech.studyName, tech.studyTitle, tech.details, tech.resources, tech.techType);
        //检查方法
        studyItem.Inspect = () =>
        {
            Inspect(studyItem, tech);
        };
        //研究方法
        studyItem.Study = () =>
        {
            Study(studyItem, tech);
        };
    }



    void Study(StudyItemManager studyItem, TechnologyBean tech)
    {
        //处理特殊科技
        Special(tech);

        switch (tech.type)
        {
            case 1://提升效率
                ImproveEfficiency(tech.revenue);
                break;
            case 2://解锁面板
                UnlockPanel(tech.revenue);
                break;
            case 3://减少资源增长
                ReduceGrowth(tech.revenue);
                break;
            case 4://增加兵营储量
                BarracksReserve(tech.revenue);
                break;
        }

    }

    /// <summary>
    /// 提升兵营的储量
    /// </summary>
    /// <param name="revenue"></param>
    void BarracksReserve(Dictionary<string, double> revenue)
    {
        foreach (var data in revenue)
        {
            BarracksManager barracksManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.Barracks).GetComponent<BarracksManager>();
            barracksManager.AddCount(int.Parse(data.Key));
        }
        BattlePanelManager.Instance.UpdateSoldierMax();
    }


    /// <summary>
    /// 特殊事件
    /// </summary>
    /// <param name="tech"></param>
    void Special(TechnologyBean tech)
    {
        if (tech.id == 1)//石镐特殊事件
        {
            //概率挖出铜
            PileOfOreManager pileOfOre = FacilityManager.Instance.GetFacilityPanel(FacilityType.Ore).GetComponent<PileOfOreManager>();
            pileOfOre.AddCopper();
        }
        if (tech.id == 5)//铜镐特殊事件
        {
            //概率挖出铁
            PileOfOreManager pileOfOre = FacilityManager.Instance.GetFacilityPanel(FacilityType.Ore).GetComponent<PileOfOreManager>();
            pileOfOre.AddIron();
        }
        if (tech.id == 6)//铜制仓库特殊事件
        {
            //需求材料增加铜矿
            FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
            //需求材料增加铜矿
            facilityPanel.AddOnClickedResource(ResourceType.Copper, 1);
        }
        if (tech.id == 24)//铁制仓库特殊事件
        {
            //需求材料增加铁矿
            FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
            //需求材料增加铜矿
            facilityPanel.AddOnClickedResource(ResourceType.Iron, 0.3);
        }

        if (tech.id == 25)//水泥房屋特殊事件
        {
            //房屋配方加上水泥
            FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);
            facilityPanel.AddOnClickedResource(ResourceType.Cement, 0.1);
        }
        if (tech.id == 27)//加强仓库特殊事件
        {
            FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
            facilityPanel.AddOnClickedResource(ResourceType.Iron, 0.5);
        }
        if (tech.id == 52)//利息特殊事件
        {
            //开启利息事件
            BankManager bankManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.Bank).GetComponent<BankManager>();
            bankManager.OnInterest();
        }
        if (tech.id == 145)//军事化特殊事件
        {
            //激活战斗
            BattlePanelManager.Instance.Activate();
        }
        if (tech.id == 206)//火星特殊事件
        {
            //激活战斗
            MarsPanelManager.Instance.Activate();
        }
        if (tech.id == 247)//深空信标事件
        {
            //激活战斗
            DeepSpacePanelManager.Instance.Activate();
        }


    }
    void UnlockPanel(Dictionary<string, double> revenue)
    {
        foreach (var item in revenue)
        {
            FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityTypeHelper.StringToFacilityType(item.Key));
            facility.gameObject.SetActive(true);
        }


    }

    void ReduceGrowth(Dictionary<string, double> revenue)
    {
        foreach (var item in revenue)
        {
            FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityTypeHelper.StringToFacilityType(item.Key));
            facility.AddUpMultiple(item.Value);
        }

    }


    /// <summary>
    /// 提升效率
    /// </summary>
    /// <param name="revenue"></param>
    void ImproveEfficiency(Dictionary<string, double> revenue)
    {
        if (revenue != null)
        {
            foreach (var type in revenue)
            {
                switch (int.Parse(type.Key))
                {
                    case 1:
                        //挖矿工具加成
                        ResourceAdditionManager.Instance.AddTool(type.Value);
                        break;
                    case 2:
                        //采矿工人加成
                        ResourceAdditionManager.Instance.AddMiningWorker(type.Value);
                        break;
                    case 3:
                        //员工加成
                        ResourceAdditionManager.Instance.AddWorker(type.Value);
                        break;
                    case 4:
                        //矿车的额外加成
                        ResourceAdditionManager.Instance.AddMineBonus(type.Value);
                        break;
                    case 5:
                        //制造工人加成
                        ResourceAdditionManager.Instance.AddFabricator(type.Value);
                        break;
                    case 6:
                        //石料工厂加成
                        ResourceAdditionManager.Instance.AddStoneFactory(type.Value);
                        break;
                    case 7:
                        //铜矿工厂加成
                        ResourceAdditionManager.Instance.AddCopperWorks(type.Value);
                        break;
                    case 8:
                        //水泥工厂加成
                        ResourceAdditionManager.Instance.AddCementFactory(type.Value);
                        break;
                    case 9:
                        //合金工厂加成
                        ResourceAdditionManager.Instance.AddAlloyFactory(type.Value);
                        break;
                    case 10:
                        //采集器加成
                        ResourceAdditionManager.Instance.AddCollectorMark(type.Value);
                        break;
                    case 11:
                        //科技点加成
                        ResourceAdditionManager.Instance.AddScience(type.Value);
                        break;
                    case 12:
                        //科技点上限
                        ResourceAdditionManager.Instance.AddTechnological(type.Value);
                        break;
                    case 13:
                        //工厂加成
                        ResourceAdditionManager.Instance.AddFactory(type.Value);
                        break;
                    case 14:
                        //石矿专项加成
                        ResourceAdditionManager.Instance.AddMinerStone(type.Value);
                        break;
                    case 15:
                        //铜矿专项加成
                        ResourceAdditionManager.Instance.AddMinerCopper(type.Value);
                        break;
                    case 16:
                        //铁矿专项加成
                        ResourceAdditionManager.Instance.AddIronMine(type.Value);
                        break;
                    case 17:
                        //水泥的专项加成
                        ResourceAdditionManager.Instance.AddCement(type.Value);
                        break;
                    case 18:
                        //钢的专项加成
                        ResourceAdditionManager.Instance.AddSteel(type.Value);
                        break;
                    case 19:
                        //铝的专项加成
                        ResourceAdditionManager.Instance.AddAluminum(type.Value);
                        break;
                    case 20:
                        //钛的专项加成
                        ResourceAdditionManager.Instance.AddTitanium(type.Value);
                        break;
                    case 21:
                        //秘银的专项加成
                        ResourceAdditionManager.Instance.AddMithril(type.Value);
                        break;
                    case 22:
                        //中子专项加成
                        ResourceAdditionManager.Instance.AddNeutron(type.Value);
                        break;
                    case 23:
                        //房屋坚固程度加成
                        ResourceAdditionManager.Instance.AddTenementComfort(type.Value);
                        break;
                    case 24:
                        //房屋基础加成
                        ResourceAdditionManager.Instance.AddTenementBasics(type.Value);
                        break;
                    case 25:
                        //房屋房租加成
                        ResourceAdditionManager.Instance.AddTenementRent(type.Value);
                        break;
                    case 26:
                        //酒馆软妹币加成
                        ResourceAdditionManager.Instance.AddTavernrmb(type.Value);
                        break;
                    case 27:
                        //房屋软妹币储量加成
                        ResourceAdditionManager.Instance.AddTenementSaveMoney(type.Value);
                        break;
                    case 28:
                        //集装箱加成
                        ResourceAdditionManager.Instance.AddContainer(type.Value);
                        break;
                    case 29:
                        //软妹币总产量
                        ResourceAdditionManager.Instance.AddRMBboost(type.Value);
                        break;
                    case 30:
                        //挖掘机提供的加成的加成
                        ResourceAdditionManager.Instance.AddExcavator(type.Value);
                        break;
                    case 31:
                        //银行储量加成
                        ResourceAdditionManager.Instance.AddBankReserveSurcharge(type.Value);
                        break;
                    case 32:
                        //所有储量加成
                        ResourceAdditionManager.Instance.AddAllReserves(type.Value);
                        break;
                    case 33:
                        //重生晶体对储量上限效果提升
                        ResourceAdditionManager.Instance.AddRegenerateCrystalSpaceBonus(type.Value);
                        break;
                    case 34:
                        //所有资源的产量
                        ResourceAdditionManager.Instance.AddAllAssets(type.Value);
                        break;
                    case 35:
                        //祭坛的效率提升
                        ResourceAdditionManager.Instance.AddAltar(type.Value);
                        break;
                    case 36:
                        //玩家力量加成
                        ResourceAdditionManager.Instance.AddPower(type.Value);
                        break;
                    case 37:
                        //战斗力加成
                        ResourceAdditionManager.Instance.AddCombatPower(type.Value);
                        break;
                    case 38:
                        //训练营效率
                        ResourceAdditionManager.Instance.AddTrainingCampEfficiency(type.Value);
                        break;
                    case 39:
                        //寺庙效率
                        ResourceAdditionManager.Instance.AddTemple(type.Value);
                        break;
                    case 40:
                        //储备站加成
                        ResourceAdditionManager.Instance.AddReserve(type.Value);
                        break;
                    case 41:
                        //仓库加成
                        ResourceAdditionManager.Instance.AddStash(type.Value);
                        break;
                    case 42:
                        //火星研究站的效率
                        ResourceAdditionManager.Instance.AddMarsResearch(type.Value);
                        break;
                    case 43:
                        //月球物资站加成
                        ResourceAdditionManager.Instance.AddLunarMaterialStation(type.Value);
                        break;
                    case 44:
                        //旅游加成
                        ResourceAdditionManager.Instance.AddTravel(type.Value);
                        break;
                    case 45:
                        //额外殖民加成
                        ResourceAdditionManager.Instance.AddColonization(int.Parse(type.Value.ToString()));
                        break;
                    case 46:
                        //精金加成
                        ResourceAdditionManager.Instance.AddPureGold(type.Value);
                        break;
                }








            }


        }




    }





    /// <summary>
    /// 监听事件
    /// </summary>
    /// <param name="studyItem"></param>
    /// <param name="tech"></param>
    void Inspect(StudyItemManager studyItem, TechnologyBean tech)
    {

        if (!SpecialEvents(studyItem, tech))
        {
            return;
        }


        //判断前置资源
        if (tech.advanceResources != null)
        {
            foreach (ResourceType type in tech.advanceResources)
            {
                //如果有一个资源没有的话就无视
                if (!ResourceManager.Instance.IsResourceUnlocked(type))
                {
                    return;
                }
            }
        }

        //判断前置科技
        if (tech.advanceTechType != null)
        {
            foreach (TechType type in tech.advanceTechType)
            {
                Debug.Log(type);
                //如果有一个科技没有的话就无视
                if (!TechManager.Instance.GetTechFlag(type))
                {
                    return;
                }
            }
        }

        //显示科技
        studyItem.gameObject.SetActive(true);
        //移除检查科技
        TechChecker.Instance.RemoveCheckTech(tech.techType);
        //完成后的特殊监听
        Completion(tech);
        //开启后置科技监听
        if (tech.monitorTechType != null)
        {
            foreach (TechType type in tech.monitorTechType)
            {
                TechChecker.Instance.AddCheckTech(type);//开启后置监听
            }
        }


    }


    bool SpecialEvents(StudyItemManager studyItem, TechnologyBean tech)
    {

        //矿车的特殊监听
        if (tech.techType == TechType.tramcar)
        {//铁轨管理
            FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Rail);

            if (facility.GetCount() < 50)
            {
                return false;
            }
        }

        if (tech.techType == TechType.GoDeeper)
        {
            //战斗力大于50k
            if (BattlePanelManager.Instance.GetPower() < 50000)
            {
                return false;
            }
        }





        return true;
    }
    /// <summary>
    /// 监听完成后特殊事件
    /// </summary>
    void Completion(TechnologyBean tech)
    {
        //破开石头的特殊监听
        if (tech.techType == TechType.BrokenStone)
        {
            if (!TechManager.Instance.GetTechFlag(TechType.FindLight))
            {
                LogManager.Instance.AddLog("有人在石壁上发现了一丝太阳照射的光芒！！");
                TechManager.Instance.techTypeStudyFlag[TechType.FindLight] = true;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
