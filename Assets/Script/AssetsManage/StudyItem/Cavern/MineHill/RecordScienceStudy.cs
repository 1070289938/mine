using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//科学记录室的研究
public class RecordScienceStudy : MonoBehaviour
{


    string studyName = "科学记录室";

    string details = "建造一间专门储存科学家们的实验成果的记录室\n\n科技探索塔的科技储量提升35%";

    string Successful = "科学记录室研究成功!";

    TechType techType = TechType.RecordScience;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币2M  科技点 1500
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("1500"),
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
        //提升科技探索塔35%储量
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.DiscoveryTower);

        DiscoveryTowerManager discoveryTowerManager = facilityPanel.GetComponent<DiscoveryTowerManager>();
        discoveryTowerManager.AddUp(0.35);
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
