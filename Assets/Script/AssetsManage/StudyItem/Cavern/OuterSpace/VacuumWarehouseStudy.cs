using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//真空仓库
public class VacuumWarehouseStudy : MonoBehaviour
{


    string studyName = "真空仓库";

    string details = "在太空电梯的顶部建立一个巨型的真空仓库\n\n仓库的储量提升100%";

    string Successful = "真空仓库研究成功";

    TechType techType = TechType.VacuumWarehouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("580M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("215K"),

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
        //太空铁矿船
        if (TechManager.Instance.GetTechFlag(TechType.SpaceIronShip))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(1);
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
