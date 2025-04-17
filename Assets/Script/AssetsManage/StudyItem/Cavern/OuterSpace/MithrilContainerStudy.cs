using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//秘银集装箱
public class MithrilContainerStudy : MonoBehaviour
{


    string studyName = "秘银集装箱";

    string details = "使用秘银加固的集装箱,可以大幅提升集装箱的效率\n\n集装箱效率提升35%";

    string Successful = "秘银集装箱研究成功";

    TechType techType = TechType.MithrilContainer;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("999M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
         [ResourceType.Mithril] = AssetsUtil.ParseNumber("1280"),

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
        //秘银锻造
        if (TechManager.Instance.GetTechFlag(TechType.MithrilForging))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
      
        ResourceAdditionManager.Instance.AddContainer(0.35);

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
