using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钛合金的研究
public class TitaniumAlloyStudy : MonoBehaviour
{


    string studyName = "钛合金";

    string details = "钛和铝结合可以合成钛合金,拥有十分坚硬的强度";

    string Successful = "钛合金研究成功!";

    TechType techType = TechType.TitaniumAlloy;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币800k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1200k"),
        [ResourceType.Aluminum] = AssetsUtil.ParseNumber("500"),
        [ResourceType.Titanium] = AssetsUtil.ParseNumber("300"),
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
        //铝采集器
        //钛采集器
        if (TechManager.Instance.GetTechFlag(TechType.AluminiumMining)&&
        TechManager.Instance.GetTechFlag(TechType.TitaniumCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

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
