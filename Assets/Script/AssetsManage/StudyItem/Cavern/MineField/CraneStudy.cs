using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//起重机的研究
public class CraneStudy : MonoBehaviour
{


    string studyName = "起重机";

    string details = "起重机可以更方便的运输集装箱\n\n集装箱的加成提升100%";

    string Successful = "起重机研究成功!";

    TechType techType = TechType.Crane;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币10000 ，铜矿1000
        [ResourceType.Currency] = 10000,
        [ResourceType.Steel] = 650,
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
        //集装箱
        if (TechManager.Instance.GetTechFlag(TechType.Container))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddContainer(1);
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();
        
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
