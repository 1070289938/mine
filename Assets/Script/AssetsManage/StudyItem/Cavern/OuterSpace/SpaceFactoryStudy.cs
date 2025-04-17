using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//太空工厂
public class SpaceFactoryStudy : MonoBehaviour
{


    string studyName = "太空工厂";

    string details = "在无引力环境下建造太空工厂大幅度的降低了工厂占地面积大的问题,工厂的效率提升80%";

    string Successful = "太空工厂研究成功";

    TechType techType = TechType.SpaceFactory;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5.5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("465K"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("1500"),

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
        //Ai
        if (TechManager.Instance.GetTechFlag(TechType.SpaceManufacturingIndustry))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

        ResourceAdditionManager.Instance.AddFactory(0.8);
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
