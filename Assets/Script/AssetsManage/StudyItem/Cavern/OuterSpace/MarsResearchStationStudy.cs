using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//火星研究站
public class MarsResearchStationStudy : MonoBehaviour
{


    string studyName = "火星研究站";

    string details = "火星研究站可以研究火星这个全新的世界,大幅度科技点的产出\n\n解锁火星研究站";

    string Successful = "火星研究站研究成功";

    TechType techType = TechType.MarsResearchStation;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("4G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("400K"),

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
        //进太空探索
        if (TechManager.Instance.GetTechFlag(TechType.MarsColony))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.MarsResearchStation);
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
