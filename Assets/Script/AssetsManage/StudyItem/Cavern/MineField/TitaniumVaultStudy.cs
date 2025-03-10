using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钛金库的研究
public class TitaniumVaultStudy : MonoBehaviour
{


    string studyName = "钛金库";

    string details = "将所有的银行金库升至钛材料\n\n提升所有银行50%软妹币储量";

    string Successful = "钛金库研究成功!";

    TechType techType = TechType.TitaniumVault;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币1000k ，钛200
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1000k"),
        [ResourceType.Titanium] = 200,
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
        //钛矿采集器
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
        ResourceAdditionManager.Instance.AddBankReserveSurcharge(0.5);
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();
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
