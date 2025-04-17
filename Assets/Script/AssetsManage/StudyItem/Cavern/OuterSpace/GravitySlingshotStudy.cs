using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//引力弹弓
public class GravitySlingshotStudy : MonoBehaviour
{


    string studyName = "引力弹弓";

    string details = "利用星球的引力可以更加节省原料的进行火星的殖民\n\n火星殖民地成本降低10%";

    string Successful = "引力弹弓研究成功";

    TechType techType = TechType.GravitySlingshot;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.8G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("355K"),

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
        //火星空间站
        if (TechManager.Instance.GetTechFlag(TechType.MarsSpaceStation))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.MarsColony);
        facilityPanel.AddUpMultiple(0.1);
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
