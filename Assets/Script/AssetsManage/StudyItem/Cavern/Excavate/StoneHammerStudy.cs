using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铁质大锤的研究
public class StoneHammerStudy : MonoBehaviour
{


    string studyName = "制作石制大锤";

    string details = "石制大锤可以快速的破碎大块的石头\n\n提升20%石头采集效率";

    string Successful = "石制大锤研究成功!";

    TechType techType = TechType.StoneHammer;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency] = 50;
        resources[ResourceType.Stone] = 30;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //石镐
        if (TechManager.Instance.GetTechFlag(TechType.pickaxe) &&
        TechManager.Instance.GetTechFlag(TechType.StoneMiner))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.CopperHammer);//铜制大锤
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //提升20%矿工石头产出效率
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
