using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铱金库
public class IridiumVaultStudy : MonoBehaviour
{


    string studyName = "铱金库";

    string details = "将银行的金库改装为铱金库可以大幅度的提升银行的储存效率\n\n银行储量提升50%";

    string Successful = "铱金库研究成功";

    TechType techType = TechType.IridiumVault;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("955M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
        [ResourceType.Iridium] = AssetsUtil.ParseNumber("400"),

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
        //铱矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.DependentCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

        ResourceAdditionManager.Instance.AddBankReserveSurcharge(0.45);

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
