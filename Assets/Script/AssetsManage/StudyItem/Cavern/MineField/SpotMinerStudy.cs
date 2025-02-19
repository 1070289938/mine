using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//定点采矿机的研究
public class SpotMinerStudy : MonoBehaviour
{


    string studyName = "定点采矿机";

    string details = "定点采矿机可以在工人挖矿的时候再附近辅助工人挖矿,可以大幅度的提升工人的效率\n\n采矿工人的效率提升65%";

    string Successful = "大型机器研究成功!";

    TechType techType = TechType.SpotMiner;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币400k ，钢1000 硅500
        [ResourceType.Currency] = AssetsUtil.ParseNumber("400k"),
        [ResourceType.Steel] = 1000,
        [ResourceType.Silicon] = 500,
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
        //电钻
        //互联网
        if (TechManager.Instance.GetTechFlag(TechType.ElectricDrill) &&
        TechManager.Instance.GetTechFlag(TechType.Internet))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddMiningWorker(0.65);
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
