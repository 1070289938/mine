using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//五险一金的研究
public class SocialInsuranceStudy : MonoBehaviour
{


    string studyName = "五险一金";

    string details = "交保险,可以提升银行软妹币的最大储量\n\n银行30%的最大储量";

    string Successful = "五险一金研究成功!";

    TechType techType = TechType.SocialInsurance;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币10000 
        [ResourceType.Currency] = 10000,
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
        //银行
        if (TechManager.Instance.GetTechFlag(TechType.Bank))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddBankReserveSurcharge(0.3);

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
