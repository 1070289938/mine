using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//跃迁引擎
public class TransitionEngineStudy : MonoBehaviour
{


    string studyName = "跃迁引擎";

    string details = "为了实现曲率技术这个理论,科学家们首次发明了跃迁引擎这项震惊世界的技术";

    string Successful = "跃迁引擎研究成功";

    TechType techType = TechType.TransitionEngine;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("6.8G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("505K"),
        [ResourceType.Mithril] = AssetsUtil.ParseNumber("10k"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("3200"),

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
        //曲率技术
        if (TechManager.Instance.GetTechFlag(TechType.CurvatureTechnique))
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
