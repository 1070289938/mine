using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//科技探索塔的研究
public class DiscoveryTowerStudy : MonoBehaviour
{


    string studyName = "科技探索塔";

    string details = "科技探索塔会聚集全国各地的科学家来交流探索新的技术\n\n解锁科技探索塔";

    string Successful = "科技探索塔研究成功!";

    TechType techType = TechType.DiscoveryTower;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3M 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("3M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("50"),
    };
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
        //实验室
        //合金工厂
        if (TechManager.Instance.GetTechFlag(TechType.Laboratory) &&
        TechManager.Instance.GetTechFlag(TechType.AlloyFactory))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.DiscoveryTower);
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
