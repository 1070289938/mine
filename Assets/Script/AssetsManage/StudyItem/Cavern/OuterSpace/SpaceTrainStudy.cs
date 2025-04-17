using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//太空铁
public class SpaceTrainStudy : MonoBehaviour
{


    string studyName = "太空铁";

    string details = "用太空的铁矿被古人命名为玄铁,用这种资源锻钢可以大幅度提升锻造效率\n\n钢的产量提升75%";

    string Successful = "太空铁研究成功";

    TechType techType = TechType.SpaceTrain;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("500M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("185K"),

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
      
        ResourceAdditionManager.Instance.AddSteel(0.75);

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
