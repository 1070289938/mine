using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//电弧炉的研究
public class ElectricFurnaceStudy : MonoBehaviour
{


    string studyName = "电弧炉";

    string details = "电弧炉可以更有效率的炼制钢铁\n\n钢的总产量提升20%";

    string Successful = "电弧炉研究成功!";

    TechType techType = TechType.ElectricFurnace;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3M 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5M"),
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
        //科技探索塔
        if (TechManager.Instance.GetTechFlag(TechType.DiscoveryTower))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        ResourceAdditionManager.Instance.AddSteel(0.2);
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
