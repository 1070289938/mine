using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//中子研究室
public class NeutronLaboratoryStudy : MonoBehaviour
{


    string studyName = "中子研究室";

    string details = "将中子带入研究室进行研究内部的物质,可以大幅度提升科技点的产出效率\n\n银科技点产量提升20%";

    string Successful = "中子研究室研究成功";

    TechType techType = TechType.NeutronLaboratory;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("455K"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("500"),

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
       ResourceAdditionManager.Instance.AddScience(0.2);

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
