using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//中子采集
public class NeutronAcquisitionStudy : MonoBehaviour
{


    string studyName = "中子采集";

    string details = "这种高危物质可以用特殊的材料进行采集并且保存下来,但是我们需要更加坚固并且稳定的采集器";

    string Successful = "中子采集研究成功";

    TechType techType = TechType.NeutronAcquisition;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("5G"),
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
        //高危物质研究
        if (TechManager.Instance.GetTechFlag(TechType.HighRiskSubstanceResearch))
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
