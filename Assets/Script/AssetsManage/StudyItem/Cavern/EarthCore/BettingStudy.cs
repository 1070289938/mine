using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//投注
public class BettingStudy : MonoBehaviour
{


    string studyName = "投注";

    string details = "酒馆增加投注收入\n\n提升酒馆60%效率";

    string Successful = "投注研究成功";

    TechType techType = TechType.betting;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("15K"),

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
        //钨矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.GeocentricResearch))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddTavernrmb(0.6);
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
