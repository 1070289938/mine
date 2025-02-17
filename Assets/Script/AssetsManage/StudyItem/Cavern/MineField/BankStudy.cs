using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//银行的研究
public class BankStudy : MonoBehaviour
{


    string studyName = "银行";

    string details = "可以储存大量的软妹币\n\n解锁银行";

    string Successful = "银行研究成功!";

    TechType techType = TechType.Bank;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币10000 ，水泥1500
        [ResourceType.Currency] = 10000,
        [ResourceType.Cement] = 1500,
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
        //大型机器
        //钢
        if (TechManager.Instance.GetTechFlag(TechType.LargeMachine)&&
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
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Bank);
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
