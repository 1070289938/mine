using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//加强仓库的研究
public class StrengthenWarehouseStudy : MonoBehaviour
{


    string studyName = "加强仓库";

    string details = "用铁+混凝土搭建仓库,可以让仓库更加的坚不可摧并且可以储存更多的资源\n\n提升80%仓库储量上限";

    string Successful = "加强仓库研究成功!";

    TechType techType = TechType.StrengthenWarehouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency] = AssetsUtil.ParseNumber("10k");
        resources[ResourceType.Cement] = 500;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //铁仓
        //水泥搅拌
        if (TechManager.Instance.GetTechFlag(TechType.IronWarehouse) &&
        TechManager.Instance.GetTechFlag(TechType.CementManufacture))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //提升80%仓库储量上限
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(0.8);


         facilityPanel.AddOnClickedResource(ResourceType.Iron, 0.5);
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
