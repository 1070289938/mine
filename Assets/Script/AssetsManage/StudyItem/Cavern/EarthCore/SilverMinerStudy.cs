using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//银矿工人
public class SilverMinerStudy : MonoBehaviour
{


    string studyName = "银矿工人";

    string details = "银矿是个不错的建筑材料，可塑性不错的同时也拥有相应的坚硬程度\n\n解锁银矿工人";

    string Successful = "银矿工人研究成功";

    TechType techType = TechType.SilverMiner;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 20M 科技点1500
        [ResourceType.Currency] = AssetsUtil.ParseNumber("20M"),
        [ResourceType.Science] = 1500,
        [ResourceType.Alloy] = 500,

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
        //防护服
        if (TechManager.Instance.GetTechFlag(TechType.LavaProtectiveSuit))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.SilverMiner);
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
