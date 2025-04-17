using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//太空制造业
public class SpaceManufacturingIndustryStudy : MonoBehaviour
{


    string studyName = "太空制造业";

    string details = "为了制作可以装载跃迁引擎这种庞然大物的飞船,科学家们决定想办法在无引力的环境下制造建筑";

    string Successful = "太空制造业研究成功";

    TechType techType = TechType.SpaceManufacturingIndustry;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("8G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("410K"),

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
        //月球探索
        if (TechManager.Instance.GetTechFlag(TechType.TransitionEngine))
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
