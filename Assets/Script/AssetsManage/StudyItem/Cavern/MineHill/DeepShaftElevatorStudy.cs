using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//深井电梯的研究
public class DeepShaftElevatorStudy : MonoBehaviour
{


    string studyName = "深井电梯";

    string details = "需要搭建深井电梯才可以到达地心深处\n\n解锁深井电梯";

    string Successful = "深井电梯研究成功!";

    TechType techType = TechType.DeepShaftElevator;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币10M、
        [ResourceType.Currency] = AssetsUtil.ParseNumber("10M"),
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
        //地心计划
        if (TechManager.Instance.GetTechFlag(TechType.GeocentricProject))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //开启深井电梯
        FacilityPanelManager facilityPanelManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.DeepShaftElevator);
        facilityPanelManager.gameObject.SetActive(true);
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
