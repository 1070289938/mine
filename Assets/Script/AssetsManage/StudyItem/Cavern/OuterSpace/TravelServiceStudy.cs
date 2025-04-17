using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//旅游一条龙服务
public class TravelServiceStudy : MonoBehaviour
{


    string studyName = "旅游一条龙服务";

    string details = "提升旅游站的服务质量,并且加入旅游套餐,可以方便一键购入可能需要的东西\n\n旅游站的收入提升50%";

    string Successful = "旅游一条龙服务研究成功";

    TechType techType = TechType.TravelService;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("360K"),

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
        //火星旅游站
        if (TechManager.Instance.GetTechFlag(TechType.MarsTouristStation))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
      
        ResourceAdditionManager.Instance.AddTravel(0.5);

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
