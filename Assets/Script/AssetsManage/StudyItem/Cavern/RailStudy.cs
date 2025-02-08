using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铁轨的研究
public class RailStudy : MonoBehaviour
{


    string studyName = "铁轨";

    string details = "用一堆石头和铁矿可以铺出一条铁路直接通往仓库";

    string Successful = "铁轨研究成功!";

    TechType techType = TechType.rail;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency] = 2000;
        resources[ResourceType.Stone] = 1600;
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
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.Iron))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.tramcar);//矿车
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //解锁铁轨 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Rail);
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
