using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钛制大锤的研究
public class TitaniumHammerStudy : MonoBehaviour
{


    string studyName = "钛制大锤";

    string details = "钛制大锤可以更加高效的采集石头\n\n石头的采集效率提升23%";

    string Successful = "钛制大锤研究成功!";

    TechType techType = TechType.TitaniumHammer;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币1200k  钛 50
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1200k"),
        [ResourceType.Titanium] = AssetsUtil.ParseNumber("50"),
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
        //钛制采集器
        if (TechManager.Instance.GetTechFlag(TechType.TitaniumCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件 提升23%石头产量
        ResourceAdditionManager.Instance.AddMinerStone(0.23);
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
