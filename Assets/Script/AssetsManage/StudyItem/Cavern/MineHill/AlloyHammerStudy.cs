using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//合金大锤的研究
public class AlloyHammerStudy : MonoBehaviour
{


    string studyName = "合金大锤";

    string details = "合金大锤可以更有效率的提升石矿的采集速度\n\n石矿工人的采集效率提升20%";

    string Successful = "合金大锤研究成功!";

    TechType techType = TechType.AlloyHammer;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3000k 合金90
        [ResourceType.Currency] = AssetsUtil.ParseNumber("3000k"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("90"),
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
        //合金工厂
        if (TechManager.Instance.GetTechFlag(TechType.AlloyFactory))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddMinerStone(0.2);
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
