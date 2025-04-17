using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//中子采集器
public class NeutronCollectorStudy : MonoBehaviour
{


    string studyName = "中子采集器";

    string details = "中子采集器十分的稳定坚固,就算是核弹炸下来也不会导致中子物质的爆炸\n\n解锁中子采集器";

    string Successful = "中子采集器研究成功";

    TechType techType = TechType.NeutronCollector;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("425K"),
        [ResourceType.Mithril] = AssetsUtil.ParseNumber("2500"),

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
        //中子采集
        if (TechManager.Instance.GetTechFlag(TechType.NeutronAcquisition))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.NeutronCollector);
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
