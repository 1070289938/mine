using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//合金仓库的研究
public class AlloyWarehouseStudy : MonoBehaviour
{


    string studyName = "合金仓库";

    string details = "合金仓库可以拥有更加庞大的储存空间\n\n提升仓库50%储量上限";

    string Successful = "合金仓库研究成功!";

    TechType techType = TechType.AlloyWarehouse;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3000k 合金800
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5800k"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("800"),
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
        //合金工厂
        if (TechManager.Instance.GetTechFlag(TechType.AlloyFactory))
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
        stashManager.AddUp(0.5);



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
