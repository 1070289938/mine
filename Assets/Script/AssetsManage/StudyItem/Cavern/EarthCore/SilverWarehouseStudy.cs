using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//银制仓库
public class SilverWarehouseStudy : MonoBehaviour
{


    string studyName = "银制仓库";

    string details = "使用银来装修一下仓库\n\n提升仓库36%储量";

    string Successful = "银制仓库研究成功";

    TechType techType = TechType.SilverWarehouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("15M"),
        [ResourceType.Science] = 8000,
        [ResourceType.silver] = AssetsUtil.ParseNumber("1.5M"),

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
        //银矿工人
        if (TechManager.Instance.GetTechFlag(TechType.SilverMiner))
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
        stashManager.AddUp(0.36);
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
