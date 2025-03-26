using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//高性能计算机
public class HighPerformanceComputerStudy : MonoBehaviour
{


    string studyName = "高性能计算机";

    string details = "高性能计算机可以储存更加多的数据\n\n科技探索塔储量提高50%";

    string Successful = "高性能计算机研究成功";

    TechType techType = TechType.HighPerformanceComputer;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("35M"),
        [ResourceType.Science] = 10000,
        [ResourceType.Nickel] = 500,

    }; //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;


        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //镍矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.NickelHarvester))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.DiscoveryTower);

        DiscoveryTowerManager discoveryTowerManager = facilityPanel.GetComponent<DiscoveryTowerManager>();
        discoveryTowerManager.AddUp(0.5);
    }


    // Update is called once per frame
    void Update()
    {

    }

    //点击事件
    void onClick()
    {
        studyItemManager.ShowItemMsg();

    }


}
