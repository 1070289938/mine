using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钨制仓库
public class TungstenWarehouseStudy : MonoBehaviour
{


    string studyName = "钨制仓库";

    string details = "使用钨来装修仓库,使他更加的坚固耐用\n\n提升仓库40%储量";

    string Successful = "钨制仓库研究成功";

    TechType techType = TechType.TungstenWarehouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30M"),
        [ResourceType.Science] = 6000,
        [ResourceType.Tungsten] = 1000,

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
        //钨矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.TungstenHarvester))
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
        stashManager.AddUp(0.4);
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
