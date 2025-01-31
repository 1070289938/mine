using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铜矿工人的研究
public class SaveMoneyStudy : MonoBehaviour
{


    string studyName = "制作储钱罐";

    string details = "工人们可以把多余的工资放进储钱罐保管\n\n增加房屋50%软妹币储量";

    string Successful = "储钱罐研究成功!";

    TechType techType = TechType.SaveMoney;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = 1000;
        resources[ResourceType.Copper] = 200;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //软妹币
        //铜
        //房屋
        //收租
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.Copper) &&
        ResourceManager.Instance.IsResourceUnlocked(ResourceType.Currency) &&
        TechManager.Instance.GetTechFlag(TechType.house) &&
        TechManager.Instance.GetTechFlag(TechType.collectRents))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.MoneyBox);//储钱箱
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        LogManager.Instance.AddLog(Successful);

        //增加房屋50%软妹币储量

        ResourceAdditionManager.Instance.AddTenementSaveMoney(0.5);
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限

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
