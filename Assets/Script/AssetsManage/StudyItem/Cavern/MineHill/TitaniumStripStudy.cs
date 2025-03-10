using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钛板条的研究
public class TitaniumStripStudy : MonoBehaviour
{


    string studyName = "钛板条";

    string details = "用钛板条来加固集装箱,提升集装箱的储存量\n\n集装箱对储量的提升提高100%";

    string Successful = "钛板条研究成功!";

    TechType techType = TechType.TitaniumStrip;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币1500k 钛500
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1500k"),
        [ResourceType.Titanium] = AssetsUtil.ParseNumber("500"),
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
        //集装箱加成提升100%
        ResourceAdditionManager.Instance.AddContainer(1);
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
