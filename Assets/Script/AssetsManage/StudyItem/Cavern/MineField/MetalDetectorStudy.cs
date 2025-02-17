using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//金属探测仪的研究
public class MetalDetectorStudy : MonoBehaviour
{


    string studyName = "金属探测仪";

    string details = "金属探测仪可以快速的探测到底下的金属,可以大幅度提升铁的产量\n\n提升50%铁的产量";

    string Successful = "金属探测仪研究成功!";

    TechType techType = TechType.MetalDetector;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币35k ，钢300 硅100
        [ResourceType.Currency] = AssetsUtil.ParseNumber("35k"),
        [ResourceType.Steel] = 300,
        [ResourceType.Silicon] = 100,
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
        //互联网
        if (TechManager.Instance.GetTechFlag(TechType.Internet))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddIronMine(0.5);
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
