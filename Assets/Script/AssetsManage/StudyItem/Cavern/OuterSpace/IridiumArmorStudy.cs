using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铱制武装
public class IridiumArmorStudy : MonoBehaviour
{


    string studyName = "铱制武装";

    string details = "用铱来强化防具是个不错的选择\n\n战斗力提升35%";

    string Successful = "铱制武装研究成功";

    TechType techType = TechType.IridiumArmor;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("955M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
        [ResourceType.Iridium] = AssetsUtil.ParseNumber("1200"),

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

       ResourceAdditionManager.Instance.AddCombatPower(0.35);
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
