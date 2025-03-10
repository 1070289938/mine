using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//储备规划的研究
public class ReservePlanningStudy : MonoBehaviour
{


    string studyName = "储备规划";

    string details = "根据科学家的建议重新规划一下工业储备站的物资存放方式,大幅的提高工业储备站的物资总储量\n\n工业储备站储量提升50%";

    string Successful = "储备规划研究成功!";

    TechType techType = TechType.ReservePlanning;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3M 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5M"),
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
        //科技探索塔
        if (TechManager.Instance.GetTechFlag(TechType.DiscoveryTower))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //提升工业储备站50%储量上限
        FacilityPanelManager industrialReserveStation = FacilityManager.Instance.GetFacilityPanel(FacilityType.IndustrialReserveStation);

        IndustrialReserveStationManager industrialReserveStationManager = industrialReserveStation.GetComponent<IndustrialReserveStationManager>();
        industrialReserveStationManager.AddUp(0.5);

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
