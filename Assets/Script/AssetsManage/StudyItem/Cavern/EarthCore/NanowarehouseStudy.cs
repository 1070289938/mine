using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//纳米仓库
public class NanowarehouseStudy : MonoBehaviour
{


    string studyName = "纳米仓库";

    string details = "使用纳米材料来对仓库进行升级\n\n提升仓库80%储量";

    string Successful = "纳米仓库研究成功";

    TechType techType = TechType.Nanowarehouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("35M"),
        [ResourceType.Science] =  AssetsUtil.ParseNumber("28k"),
        [ResourceType.Nanomaterials] = 800,

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
        //纳米工厂
        if (TechManager.Instance.GetTechFlag(TechType.Nanofactory))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(0.8);
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
