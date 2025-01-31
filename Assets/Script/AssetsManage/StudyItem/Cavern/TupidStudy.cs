using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铁质大锤的研究
public class TupidStudy : MonoBehaviour
{


    string studyName = "制作铁制大锤";

    string details = "铁制大锤坚硬无比,比之前的材料效果更加明显\n\n提升100%石头采集效率";

    string Successful = "铁质大锤研究成功!";

    TechType techType = TechType.tupid;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = 2000;
        resources[ResourceType.Iron] = 100;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //铁镐
        if (TechManager.Instance.GetTechFlag(TechType.ironPickaxe))
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
