using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铁质仓库的研究
public class IronWarehouseStudy : MonoBehaviour
{


    string studyName = "搭建铁质仓库";

    string details = "用铁块搭建的仓库更加的坚不可摧,并且可以容下更多的资源\n\n提升仓库50%储量上限";

    string Successful = "铁质仓库研究成功!";

    TechType techType = TechType.IronWarehouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = 5000;
        resources[ResourceType.Iron] = 300;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //铁矿
        //铜仓
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.Iron) &&
            TechManager.Instance.GetTechFlag(TechType.CopperWarehouse))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //提升50%仓库储量
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(0.5);

        //需求材料增加铁矿
        facilityPanel.AddOnClickedResource(ResourceType.Iron, 3);
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
