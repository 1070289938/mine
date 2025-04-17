using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//火星空间站
public class MarsSpaceStationStudy : MonoBehaviour
{


    string studyName = "火星空间站";

    string details = "部署火星空间站辅助火星殖民地的发展,可以提升火星殖民地的殖民点数\n\n解锁火星空间站";

    string Successful = "火星空间站研究成功";

    TechType techType = TechType.MarsSpaceStation;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2.5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("425K"),


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
        //火星殖民地
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
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.MarsSpaceStation);
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
