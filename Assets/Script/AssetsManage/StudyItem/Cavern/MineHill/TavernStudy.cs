using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//酒馆的研究
public class TavernStudy : MonoBehaviour
{


    string studyName = "酒馆";

    string details = "酒馆可以为你带来软妹币的收入的同时也可以提升所有房屋的产量";

    string Successful = "酒馆研究成功!";

    TechType techType = TechType.Tavern;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币1200k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1200k"),
    };
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
        //银行，工厂建设
        if (TechManager.Instance.GetTechFlag(TechType.Bank)&&
        TechManager.Instance.GetTechFlag(TechType.PlantConstruction))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tavern);
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
