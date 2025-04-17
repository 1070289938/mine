using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//智能机床
public class IntelligentMachineToolStudy : MonoBehaviour
{


    string studyName = "智能机床";

    string details = "使用AI来加强机床的效率,可以大幅度的提升制造工人的效率\n\n制造工人的效率提升25%";

    string Successful = "智能机床研究成功";

    TechType techType = TechType.IntelligentMachineTool;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2.9G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("355K"),
       

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
        //Ai
        if (TechManager.Instance.GetTechFlag(TechType.AI))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

       ResourceAdditionManager.Instance.AddFabricator(0.25);
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
