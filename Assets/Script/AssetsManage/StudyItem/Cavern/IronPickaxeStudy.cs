using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铁镐的研究
public class IronPickaxeStudy : MonoBehaviour
{


    string studyName = "制作铁镐";

    string details = "我们发现铁比之前任何的矿物都坚硬，我认为用它作为镐子的材料可以使镐子更加的耐用\n\n提升30%采矿效率";

    string Successful = "仓库研究成功!";

    TechType techType = TechType.ironPickaxe;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = 1500;
        resources[ResourceType.Iron] = 50;

        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //铁矿
        //铜镐
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.Iron) &&
        TechManager.Instance.GetTechFlag(TechType.BrassPick))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.tupid);//铁制大锤
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //提升30%工具提升
        ResourceAdditionManager.Instance.AddTool(0.3);
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
