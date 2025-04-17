using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//中子金库
public class ChineseTreasuryStudy : MonoBehaviour
{


    string studyName = "中子金库";

    string details = "使用中子强化银行的金库,可以大幅度的提升金库的储量\n\n银行的储量提升60%";

    string Successful = "中子金库研究成功";

    TechType techType = TechType.ChineseTreasury;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("455K"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("200"),

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
        //中子采集器
        if (TechManager.Instance.GetTechFlag(TechType.NeutronCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       ResourceAdditionManager.Instance.AddBankReserveSurcharge(0.6);

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
