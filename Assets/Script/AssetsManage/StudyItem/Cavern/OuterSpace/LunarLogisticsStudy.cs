using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//月球物流
public class LunarLogisticsStudy : MonoBehaviour
{


    string studyName = "月球物流";

    string details = "将物流扩展到月球,可以大幅提升员工的效率\n\n员工效率提升38%";

    string Successful = "月球物流研究成功";

    TechType techType = TechType.LunarLogistics;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.4G"),
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

        ResourceAdditionManager.Instance.AddWorker(0.38);

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
