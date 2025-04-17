using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//精化秘银
public class RefinedMithrilStudy : MonoBehaviour
{


    string studyName = "精化秘银";

    string details = "精化秘银,AI提出一个全新的锻造秘银方式\n\n秘银的生产效率提升35%";

    string Successful = "精化秘银研究成功";

    TechType techType = TechType.RefinedMithril;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("4G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("385K"),
       

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

       ResourceAdditionManager.Instance.AddMithril(0.35);
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
