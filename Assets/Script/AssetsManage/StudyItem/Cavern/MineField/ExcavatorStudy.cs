using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//挖掘机的研究
public class ExcavatorStudy : MonoBehaviour
{


    string studyName = "挖掘机";

    string details = "挖掘机可以大幅度的提升石矿工人的工作效率\n\n解锁挖掘机";

    string Successful = "挖掘机研究成功!";

    TechType techType = TechType.Excavator;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币120k ，铜矿300
        [ResourceType.Currency] = AssetsUtil.ParseNumber("120k"),
        [ResourceType.Steel] = 300,
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
        //大型机器
        //钻头
        if (TechManager.Instance.GetTechFlag(TechType.LargeMachine) &&
        TechManager.Instance.GetTechFlag(TechType.DrillBit))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Excavator);
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
