using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//火星旅游
public class InterstellarTourismStudy : MonoBehaviour
{


    string studyName = "火星旅游";

    string details = "有富人提出想去火星进行旅游的计划,我们可以考虑一下发展一下火星旅游业";

    string Successful = "火星旅游研究成功";

    TechType techType = TechType.InterstellarTourism;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("355K"),

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
        //火星殖民地
        if (TechManager.Instance.GetTechFlag(TechType.MarsColony))
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
