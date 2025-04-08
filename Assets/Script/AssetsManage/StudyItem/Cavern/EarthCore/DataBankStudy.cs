using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//地心资料库
public class DataBankStudy : MonoBehaviour
{


    string studyName = "地心资料库";

    string details = "将地心的资料收集起来上传到科技探索塔,用于提升科技上限\n\n每个地心研究所都会提升科技探索塔的储量上限";

    string Successful = "地心资料库研究成功";

    TechType techType = TechType.DataBank;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("90M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("20k"),
        [ResourceType.Nanomaterials] = 800,

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
        //地心研究所
        //纳米工厂
        if (TechManager.Instance.GetTechFlag(TechType.GeocentricResearch)&&
        TechManager.Instance.GetTechFlag(TechType.Nanofactory))
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
