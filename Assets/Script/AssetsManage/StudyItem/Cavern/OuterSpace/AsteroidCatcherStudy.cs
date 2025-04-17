using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//小行星捕获器
public class AsteroidCatcherStudy : MonoBehaviour
{


    string studyName = "小行星捕获器";

    string details = "在太空电梯附近安装小行星捕获器,用于大幅度提升铁矿的产量\n\n铁的产量提升50%";

    string Successful = "小行星捕获器研究成功";

    TechType techType = TechType.AsteroidCatcher;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("300M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("215K"),

    }; //研究需要的资源
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
        //太空铁矿船
        if (TechManager.Instance.GetTechFlag(TechType.SpaceIronShip))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
      
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
