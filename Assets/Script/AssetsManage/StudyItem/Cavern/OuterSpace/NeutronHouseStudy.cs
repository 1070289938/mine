using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//中子屋
public class NeutronHouseStudy : MonoBehaviour
{


    string studyName = "中子屋";

    string details = "使用中子强化房屋,使其更加的稳定\n\n房屋的成本-20%";

    string Successful = "中子屋研究成功";

    TechType techType = TechType.NeutronHouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("4.6G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("425K"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("200"),

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
        //中子采集器
        if (TechManager.Instance.GetTechFlag(TechType.NeutronCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);
        facilityPanel.AddUpMultiple(0.2);

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
