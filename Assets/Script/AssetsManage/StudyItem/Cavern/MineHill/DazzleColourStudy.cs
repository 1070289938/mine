using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//灯红酒绿的研究
public class DazzleColourStudy : MonoBehaviour
{


    string studyName = "灯红酒绿";

    string details = "用花哨的装饰来装潢酒馆\n\n酒馆的收入提升30%";

    string Successful = "灯红酒绿研究成功!";

    TechType techType = TechType.DazzleColour;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3M 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("3.5M"),
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
        ResourceAdditionManager.Instance.AddTavernrmb(0.3);
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
