using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//手持电钻的研究
public class ElectricDrillStudy : MonoBehaviour
{


    string studyName = "电钻";

    string details = "比钻头更好用的电钻\n\n挖矿效率提升100%";

    string Successful = "电钻研究成功!";

    TechType techType = TechType.ElectricDrill;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币30000 ，钢500 硅50
        [ResourceType.Currency] = 30000,
        [ResourceType.Steel] = 500,
        [ResourceType.Silicon] = 50,
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
        //钻头
        //硅
        if (TechManager.Instance.GetTechFlag(TechType.DrillBit) &&
        TechManager.Instance.GetTechFlag(TechType.SiliconMining))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddTool(1);
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
