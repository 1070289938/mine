using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//奢侈品的研究
public class LuxuryStudy : MonoBehaviour
{


    string studyName = "奢侈品";

    string details = "酒馆会出售一些奢侈品给工人,进一步的提升酒馆的收入,酒馆的软妹币收入提升100%";

    string Successful = "奢侈品研究成功!";

    TechType techType = TechType.Luxury;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币1500k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1500k"),
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
        //酒馆
        if (TechManager.Instance.GetTechFlag(TechType.Tavern))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddTavernrmb(1);
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
