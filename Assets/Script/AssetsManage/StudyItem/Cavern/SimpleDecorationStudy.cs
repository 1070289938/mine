using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//简陋的家具
public class SimpleDecorationStudy : MonoBehaviour
{


    string studyName = "简陋的家具";

    string details = "在房屋里面装上简陋的家具来提高房屋的基础软妹币产量\n\n提升20%房屋基础软妹币产量";

    string Successful = "简陋的家具研究成功!";

    TechType techType = TechType.SimpleDecoration;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
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
        //铜矿
        //收租
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.Copper) &&
             TechManager.Instance.GetTechFlag(TechType.collectRents))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件 提升20%基础产量
        ResourceAdditionManager.Instance.AddTenementBasics(0.2);


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
