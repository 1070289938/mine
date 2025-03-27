using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//新兵训练营
public class BootCampStudy : MonoBehaviour
{


    string studyName = "新兵训练营";

    string details = "新兵训练可以更快的训练士兵来快速的回复战斗力\n\n解锁新兵训练营";

    string Successful = "新兵训练营研究成功";

    TechType techType = TechType.BootCamp;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("21M"),
        [ResourceType.Science] = 20000,
        [ResourceType.GeocentricRock] = AssetsUtil.ParseNumber("2M"),

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
        //兵营
        if (TechManager.Instance.GetTechFlag(TechType.Barracks))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.BootCamp);
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
