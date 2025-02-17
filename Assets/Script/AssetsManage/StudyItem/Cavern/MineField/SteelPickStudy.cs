using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钢镐的研究
public class SteelPickStudy : MonoBehaviour
{


    string studyName = "钢镐";

    string details = "把坚硬的钢熔炼成镐子,可以大幅度提升矿物的挖掘效率\n\n提升35%采矿效率";

    string Successful = "钢镐研究成功!";

    TechType techType = TechType.SteelPick;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new()
    {
        //设置价格 软妹币5000 ，钢 50
        [ResourceType.Currency] = 5000,
        [ResourceType.Steel] = 50,
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
        //钢铁
        //铁镐
        if (TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel) &&
        TechManager.Instance.GetTechFlag(TechType.ironPickaxe))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddTool(0.35);
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
