using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//太空电梯
public class SpaceElevatorStudy : MonoBehaviour
{


    string studyName = "太空电梯";

    string details = "科学家们提出了太空电梯的理论,只需要用超高强度的材料进行堆叠上天,应该就可以到达太空(太空电梯建立在矿山区域)";

    string Successful = "太空电梯研究成功";

    TechType techType = TechType.SpaceElevator;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("155M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("60K"),

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
          //登天计划
        if (TechManager.Instance.GetTechFlag(TechType.LandingPlan))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.SpaceElevator);
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
