using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铱钻
public class IridiumDiamondStudy : MonoBehaviour
{


    string studyName = "铱钻";

    string details = "铱钻的挖掘效率更高\n\n挖矿效率提升20%";

    string Successful = "铱钻研究成功";

    TechType techType = TechType.IridiumDiamond;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("655M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
         [ResourceType.Iridium] = AssetsUtil.ParseNumber("280"),

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
      
        ResourceAdditionManager.Instance.AddTool(0.2);

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
