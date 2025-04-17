using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//AI导游
public class AITourGuideStudy : MonoBehaviour
{


    string studyName = "AI导游";

    string details = "将AI装入火星旅游站,可以大幅度的提升火星旅游站的效率\n\n火星旅游站的效率提升35%";

    string Successful = "AI导游研究成功";

    TechType techType = TechType.AITourGuide;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("3.5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
       

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

       ResourceAdditionManager.Instance.AddTravel(0.35);
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
