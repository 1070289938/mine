using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Ai
public class AIStudy : MonoBehaviour
{


    string studyName = "AI";

    string details = "全新的科技,人工智能的升级版AI";

    string Successful = "AI研究成功";

    TechType techType = TechType.AI;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.6G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("325K"),
       

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
        //火星研究站
        if (TechManager.Instance.GetTechFlag(TechType.MarsResearchStation))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

       
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
