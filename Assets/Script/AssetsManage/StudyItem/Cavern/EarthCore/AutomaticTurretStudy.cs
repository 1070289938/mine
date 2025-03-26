using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//自动炮塔
public class AutomaticTurretStudy : MonoBehaviour
{


    string studyName = "自动炮塔";

    string details = "自动炮塔可以用于防御敌人,每个自动炮塔都会提供阵地战斗力\n\n解锁自动炮塔";

    string Successful = "自动炮塔研究成功";

    TechType techType = TechType.AutomaticTurret;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("23M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("12k"),
        [ResourceType.Tungsten] = AssetsUtil.ParseNumber("5k"),
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
        //防御阵地
        if (TechManager.Instance.GetTechFlag(TechType.DefensivePosition))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.AutomaticTurret);
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
