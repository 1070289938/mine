using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//简易的装修
public class SimpleFinishStudy : MonoBehaviour
{


    string studyName = "简易的装修";

    string details = "在屋子的墙壁上糊点混凝土,这样一来不仅能防止漏风漏水,还能提升房屋的美观程度\n\n提升25%房屋基础软妹币产量\n\n提升50%房租产量";

    string Successful = "简易的装修研究成功!";

    TechType techType = TechType.SimpleFinish;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency] = 3000;
        resources[ResourceType.Cement] = 100;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //混凝土
        //简陋家具
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.Cement) &&
             TechManager.Instance.GetTechFlag(TechType.SimpleDecoration))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件 提升25%基础产量   提升50%房租产量
        ResourceAdditionManager.Instance.AddTenementBasics(0.25);

        ResourceAdditionManager.Instance.AddTenementRent(0.5);
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
