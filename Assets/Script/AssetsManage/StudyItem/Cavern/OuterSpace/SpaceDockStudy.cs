using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//太空船坞
public class SpaceDockStudy : MonoBehaviour
{


    string studyName = "太空船坞";

    string details = "这大小堪比一颗星球的太空船坞可以进行制造能搭建跃迁引擎的巨型飞船";

    string Successful = "太空工厂研究成功";

    TechType techType = TechType.SpaceDock;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2.5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("365K"),

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
        //太空制造业
        if (TechManager.Instance.GetTechFlag(TechType.SpaceManufacturingIndustry))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
            FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.SpaceDock);
            facilityPanel.gameObject.SetActive(true);
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
