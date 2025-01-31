using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铜矿工人的研究
public class MoneyBoxStudy : MonoBehaviour
{


    string studyName = "研究储钱箱";

    string details = "铁制作的储钱箱比储钱罐好用多了\n\n增加房屋100%软妹币储量";

    string Successful = "储钱箱研究成功!";

    TechType techType = TechType.MoneyBox;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = 10000;
        resources[ResourceType.Iron] = 500;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //铁仓库
        //存钱罐
        if (TechManager.Instance.GetTechFlag(TechType.IronWarehouse) &&
        TechManager.Instance.GetTechFlag(TechType.SaveMoney))
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

        //增加房屋100%软妹币储量

        ResourceAdditionManager.Instance.AddTenementSaveMoney(1);
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
