using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//近太空探索
public class NearSpaceExplorationStudy : MonoBehaviour
{


    string studyName = "近太空探索";

    string details = "太空电梯搭建完毕后派遣一个飞船小队探索太空这个新世界";

    string Successful = "小队发现太空中有大量的陨石,内含大量铁矿,并且探索完这个“新世界”后对月球产生了兴趣";

    TechType techType = TechType.NearSpaceExploration;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("200M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("190K"),

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
        //探索痕迹
        if (TechManager.Instance.GetTechFlag(TechType.CompletionConstruction))
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
