using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//水泥房屋的研究
public class ConcreteBuildingStudy : MonoBehaviour
{


    string studyName = "用水泥搭建房屋";

    string details = "水泥搭建的房屋不仅坚固,而且不会漏风可以保证居民的安全和舒适\n\n提升房屋30%软妹币产量";

    string Successful = "水泥房屋研究成功!";

    TechType techType = TechType.ConcreteBuilding;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = 5800;
        resources[ResourceType.Cement] = 100;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //水泥搅拌工
        if (TechManager.Instance.GetTechFlag(TechType.CementManufacture))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.shore);//支撑柱
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //房屋坚固程度提升30%
        ResourceAdditionManager.Instance.AddTenementComfort(0.3);

        //房屋配方加上水泥
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);
        facilityPanel.AddOnClickedResource(ResourceType.Cement, 1);

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
