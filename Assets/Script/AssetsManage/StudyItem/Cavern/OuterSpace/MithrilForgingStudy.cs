using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//秘银锻造
public class MithrilForgingStudy : MonoBehaviour
{


    string studyName = "秘银锻造";

    string details = "铱矿有强大的锻造潜力,与银有着极强的复合力\n\n解锁秘银锻造厂";

    string Successful = "秘银锻造研究成功";

    TechType techType = TechType.MithrilForging;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("955M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
        [ResourceType.Iridium] = AssetsUtil.ParseNumber("900"),

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
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.MithrilForge);
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
