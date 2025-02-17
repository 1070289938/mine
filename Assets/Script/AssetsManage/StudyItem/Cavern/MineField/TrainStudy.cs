using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//火车的研究
public class TrainStudy : MonoBehaviour
{


    string studyName = "火车";

    string details = "火车可以大幅度的提升资源的运输效率的同时还可以提升收益来往\n\n提升50%仓库储量,提升软妹币20%产量";

    string Successful = "火车研究成功!";

    TechType techType = TechType.Train;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币20k ，煤600，钢200
        [ResourceType.Currency] = 20000,
        [ResourceType.Colliery] = 600,
        [ResourceType.Steel] = 200,
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
        //破开石头
        //矿车
        //钢铁炼制
        if (TechManager.Instance.GetTechFlag(TechType.BrokenStone) &&
        TechManager.Instance.GetTechFlag(TechType.tramcar) &&
        TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddRMBboost(0.2);

        //提升仓库50%容量
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
