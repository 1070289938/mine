using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//高精密仪器
public class HighPrecisionInstrumentStudy : MonoBehaviour
{


    string studyName = "高精密仪器";

    string details = "使用钨来制造高精密仪器,可以使制造工人的效率提升\n\n提升制造工人50%效率";

    string Successful = "高精密仪器研究成功";

    TechType techType = TechType.HighPrecisionInstrument;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30M"),
        [ResourceType.Science] = 8000,
        [ResourceType.Tungsten] = 3000,

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
        //钨矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.TungstenHarvester))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        ResourceAdditionManager.Instance.AddFabricator(0.5);
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
