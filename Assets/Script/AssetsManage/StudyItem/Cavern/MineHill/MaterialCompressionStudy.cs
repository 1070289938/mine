using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//物质压缩的研究
public class MaterialCompressionStudy : MonoBehaviour
{


    string studyName = "物质压缩";

    string details = "将集装箱利用上物质压缩的技术,可以大幅度的提升集装箱的储物量\n\n集装箱对储物的提升提高100%";

    string Successful = "物质压缩研究成功!";

    TechType techType = TechType.MaterialCompression;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3M  佐里旬矿200
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5M"),
        [ResourceType.Zorizun] = AssetsUtil.ParseNumber("200"),
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
        //佐里旬矿探测器
        if (TechManager.Instance.GetTechFlag(TechType.ZoriMineDetector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
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
