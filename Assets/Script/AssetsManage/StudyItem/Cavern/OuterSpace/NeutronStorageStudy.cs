using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//中子储物
public class NeutronStorageStudy : MonoBehaviour
{


    string studyName = "中子储物";

    string details = "用中子进行强化集装箱,稳固内部结构,大幅度提升集装箱的储量\n\n集装箱的储量提升60%";

    string Successful = "中子储物研究成功";

    TechType techType = TechType.NeutronStorage;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("455K"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("1200"),

    }; //研究需要的资源
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
        //中子采集器
        if (TechManager.Instance.GetTechFlag(TechType.NeutronCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       ResourceAdditionManager.Instance.AddContainer(0.6);

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
