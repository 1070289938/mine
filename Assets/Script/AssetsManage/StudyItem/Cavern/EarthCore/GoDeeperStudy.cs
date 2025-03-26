using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//继续深入
public class GoDeeperStudy : MonoBehaviour
{


    string studyName = "继续深入";

    string details = "当前已经拥有的足够的实力,你决定带着部队继续深入门内深处";

    string Successful = "雷达检测到附近有十分密集的地心犬信号,貌似是一块聚集地";

    TechType techType = TechType.GoDeeper;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("25k"),

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
        //战斗力大于100
        if (TechManager.Instance.GetTechFlag(TechType.CombatPower100))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       
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
