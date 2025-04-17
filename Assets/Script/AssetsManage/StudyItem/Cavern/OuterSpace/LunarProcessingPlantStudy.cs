using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//月球加工厂
public class LunarProcessingPlantStudy : MonoBehaviour
{


    string studyName = "月球加工厂";

    string details = "在月球发展工厂行业提升所有工厂的效率\n\n所有工厂效率提升100%";

    string Successful = "月球加工厂研究成功";

    TechType techType = TechType.LunarProcessingPlant;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.2G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("365K"),
        

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

        ResourceAdditionManager.Instance.AddFactory(1);

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
