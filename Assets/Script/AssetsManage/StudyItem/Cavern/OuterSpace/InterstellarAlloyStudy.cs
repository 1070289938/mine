using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//星际合金
public class InterstellarAlloyStudy : MonoBehaviour
{


    string studyName = "星际合金";

    string details = "在太空锻造的合金称为星际合金\n\n提升合金40%生产效率";

    string Successful = "星际合金研究成功";

    TechType techType = TechType.InterstellarAlloy;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.4G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("355K"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("20k"),

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
        //月球资料站
        if (TechManager.Instance.GetTechFlag(TechType.LunarDataStation))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

        ResourceAdditionManager.Instance.AddAlloyFactory(0.4);

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
