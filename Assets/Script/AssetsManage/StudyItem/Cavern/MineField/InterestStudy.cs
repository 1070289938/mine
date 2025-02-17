using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//利息的研究
public class InterestStudy : MonoBehaviour
{


    string studyName = "利息";

    string details = "银行会获得利息,每个屋子可以提升每个银行每秒1软妹币的产量";

    string Successful = "利息研究成功!";

    TechType techType = TechType.Interest;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币20000 
        [ResourceType.Currency] = 20000,
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
        if (TechManager.Instance.GetTechFlag(TechType.Invest))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //开启利息事件
        BankManager bankManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.Bank).GetComponent<BankManager>();
        bankManager.OnInterest();
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
