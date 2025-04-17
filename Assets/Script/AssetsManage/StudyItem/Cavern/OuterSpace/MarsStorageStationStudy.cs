using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//殖民地储物站
public class MarsStorageStationStudy : MonoBehaviour
{


    string studyName = "殖民地储物站";

    string details = "在殖民地加装储物站,可以大幅度的提升仓库,以及工业储备站的储量\n\n仓库和工业储备站储量提升45%";

    string Successful = "殖民地储物站研究成功";

    TechType techType = TechType.MarsStorageStation;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("3.9G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("425K"),


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
        //Ai
        if (TechManager.Instance.GetTechFlag(TechType.MarsResearchStation))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

        //触发研究事件
        //仓库
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(0.45);

        //工业储备站
        FacilityPanelManager industrialReserveStation = FacilityManager.Instance.GetFacilityPanel(FacilityType.IndustrialReserveStation);
        IndustrialReserveStationManager industrialReserveStationManager = industrialReserveStation.GetComponent<IndustrialReserveStationManager>();
        industrialReserveStationManager.AddUp(0.45);

       
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
