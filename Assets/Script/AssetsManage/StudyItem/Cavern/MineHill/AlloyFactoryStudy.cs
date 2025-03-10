using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//合金工厂的研究
public class AlloyFactoryStudy : MonoBehaviour
{


    string studyName = "合金工厂";

    string details = "专门用于锻造合金的工厂,可以把铝和钛锻造成为合金\n\n解锁合金工厂";

    string Successful = "合金工厂研究成功!";

    TechType techType = TechType.AlloyFactory;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币2000k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2000k"),
        [ResourceType.Titanium] = AssetsUtil.ParseNumber("500"),
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
        //钛合金
        if (TechManager.Instance.GetTechFlag(TechType.TitaniumAlloy))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.AlloyFactory);
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
