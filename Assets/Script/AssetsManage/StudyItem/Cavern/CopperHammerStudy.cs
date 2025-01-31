using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铁质大锤的研究
public class CopperHammerStudy : MonoBehaviour
{


    string studyName = "制作铜制大锤";

    string details = "铜制大锤可以更加有效率的破除石头\n\n提升50%石头采集效率";

    string Successful = "铜制大锤研究成功!";

    TechType techType = TechType.CopperHammer;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = 800;
        resources[ResourceType.Copper] = 100;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //铜镐
        //石锤
        if (TechManager.Instance.GetTechFlag(TechType.BrassPick) &&
        TechManager.Instance.GetTechFlag(TechType.StoneHammer))
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
        //提升50%矿工石头产出效率
        ResourceAdditionManager.Instance.AddMinerStone(0.5);
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
