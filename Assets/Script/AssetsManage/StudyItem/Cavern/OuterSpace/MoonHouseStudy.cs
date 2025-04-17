using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//月球房屋
public class MoonHouseStudy : MonoBehaviour
{


    string studyName = "月球房屋";

    string details = "在月球扩建房屋,降低房屋的资源需求\n\n房屋的价格提升-20%";

    string Successful = "月球房屋研究成功";

    TechType techType = TechType.MoonHouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.4G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("385K"),
        

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
        //月球资料站
        if (TechManager.Instance.GetTechFlag(TechType.LunarDataStation))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

        //获取到所有的建筑
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);

        facility.AddUpMultiple(0.2);

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
