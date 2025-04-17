using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//月球资料站
public class LunarDataStationStudy : MonoBehaviour
{


    string studyName = "月球资料站";

    string details = "搭建月球的资料站,储存关于月球的所有科学信息\n\n解锁月球资料站";

    string Successful = "月球资料站研究成功";

    TechType techType = TechType.LunarDataStation;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("955M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
        [ResourceType.Iridium] = AssetsUtil.ParseNumber("1500"),

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
        //铱矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.DependentCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.LunarDataStation);
        facility.gameObject.SetActive(true);

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
