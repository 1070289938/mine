using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//高危物质研究
public class HighRiskSubstanceResearchStudy : MonoBehaviour
{


    string studyName = "高危物质研究";

    string details = "科学家们在研究火星地理物质的时候发现了一种高危物质";

    string Successful = "高危物质研究研究成功";

    TechType techType = TechType.HighRiskSubstanceResearch;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("4.6G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("475K"),

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
        //火星地理研究
        if (TechManager.Instance.GetTechFlag(TechType.MarsGeographicalResearch))
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
