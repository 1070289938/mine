using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//火星工业
public class MarsIndustryStudy : MonoBehaviour
{


    string studyName = "火星工业";

    string details = "在火星发展工业区,大幅度提升所有工厂的效率\n\n工厂效率提升35%";

    string Successful = "火星工业研究成功";

    TechType techType = TechType.MarsIndustry;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("425K"),
         

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
        //月球物资站
        if (TechManager.Instance.GetTechFlag(TechType.MarsColony))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       
        ResourceAdditionManager.Instance.AddFactory(0.35);




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
