using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钢制大锤的研究
public class SteelSledgehammerStudy : MonoBehaviour
{


    string studyName = "钢制大锤";

    string details = "钢制大锤可以大幅度的提升石矿的采集速度\n\n石矿的采集效率提升25%";

    string Successful = "钢制大锤研究成功!";

    TechType techType = TechType.SteelSledgehammer;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币15k ，钢50
        [ResourceType.Currency] = 15000,
        [ResourceType.Steel] = 50,
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
        //钢
        if (TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddMinerStone(0.25);
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
