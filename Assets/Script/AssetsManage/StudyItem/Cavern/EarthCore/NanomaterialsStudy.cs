using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//纳米材料
public class NanomaterialsStudy : MonoBehaviour
{


    string studyName = "纳米材料";

    string details = "纳米材料在形状极其迷你的情况下拥有高强度的坚固程度";

    string Successful = "纳米材料研究成功";

    TechType techType = TechType.Nanomaterials;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30M"),
        [ResourceType.Science] = 6000,
        [ResourceType.Tungsten] = 500,
        [ResourceType.Nickel] = 480,

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
        //高精密仪器
        //镍矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.HighPrecisionInstrument) &&
        TechManager.Instance.GetTechFlag(TechType.NickelHarvester))
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
