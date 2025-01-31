using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//支撑柱的研究
public class ShoreStudy : MonoBehaviour
{


    string studyName = "安装支撑柱";

    string details = "用铁+水泥可以在房屋的重要地方安装支撑柱,可以大幅度提升房屋的坚固度并且减少房屋所需的材料\n\n房屋的建筑资源增长-10%";

    string Successful = "支撑柱研究成功!";

    TechType techType = TechType.shore;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = AssetsUtil.ParseNumber("13k");
        resources[ResourceType.Cement] = 700;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //水泥房屋
        if (TechManager.Instance.GetTechFlag(TechType.ConcreteBuilding))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //房屋的建筑资源增长-10%
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);
        facility.AddUpMultiple(0.1);
        

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
