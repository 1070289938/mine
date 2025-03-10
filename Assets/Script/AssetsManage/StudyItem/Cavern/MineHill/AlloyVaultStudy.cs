using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//合金金库的研究
public class AlloyVaultStudy : MonoBehaviour
{


    string studyName = "合金金库";

    string details = "给银行安装上合金金库,可以大幅度提升银行资金的安全系数\n\n提升银行250%基础储量";

    string Successful = "合金金库研究成功!";

    TechType techType = TechType.AlloyVault;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3000k 合金1000
        [ResourceType.Currency] = AssetsUtil.ParseNumber("3000k"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("1000"),
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
        ResourceAdditionManager.Instance.AddBankReserveSurcharge(2.5);
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
