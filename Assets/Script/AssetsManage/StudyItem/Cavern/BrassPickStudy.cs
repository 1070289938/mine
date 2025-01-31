using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铜镐的研究
public class BrassPickStudy : MonoBehaviour
{


    string studyName = "制作铜镐";

    string details = "我们发现铜拥有更高的可塑性，用它做的镐子更加的锋利实用\n\n将所有的工具替换为石镐\n\n提升20%采矿效率";

    string Successful = "铜镐研究成功!";

    TechType techType = TechType.BrassPick;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Stone] = 100;
        resources[ResourceType.Copper] = 20;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //铜矿
        //石镐
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.Copper) &&
           TechManager.Instance.GetTechFlag(TechType.pickaxe))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //提升20%工具加成
        ResourceAdditionManager.Instance.AddMiningWorker(0.2);


        //概率挖出铁
        PileOfOreManager pileOfOre = FacilityManager.Instance.GetFacilityPanel(FacilityType.Ore).GetComponent<PileOfOreManager>();
        pileOfOre.AddIron();
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
