using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//石料加工厂的研究
public class StoneFactoryStudy : MonoBehaviour
{


    string studyName = "石料加工厂";

    string details = "石矿工人可以将采集到的石头运输到石料加工厂进行二次加工,以便于更高效率的使用，每个石料加工厂都可以提升石矿工人的产量";

    string Successful = "石料加工厂研究成功!";

    TechType techType = TechType.StoneFactory;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币850k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("850k"),
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
        //工厂建设
        if (TechManager.Instance.GetTechFlag(TechType.PlantConstruction))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.StoneMill);
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
