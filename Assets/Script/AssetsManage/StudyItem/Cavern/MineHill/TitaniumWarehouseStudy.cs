using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钛制仓库的研究
public class TitaniumWarehouseStudy : MonoBehaviour
{


    string studyName = "钛制仓库";

    string details = "钛制仓库可以拥有更加庞大的储存空间\n\n提升仓库38%储量上限";

    string Successful = "钛制仓库研究成功!";

    TechType techType = TechType.TitaniumWarehouse;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3000k 钛1000
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2800k"),
        [ResourceType.Titanium] = AssetsUtil.ParseNumber("1000"),
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
        //钛采集器
        if (TechManager.Instance.GetTechFlag(TechType.TitaniumCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //提升仓库45%储量上限
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);

        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(0.38);



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
