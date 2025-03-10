using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//金属精炼厂的研究
public class MetalRefineryStudy : MonoBehaviour
{


    string studyName = "金属精炼厂";

    string details = "金属精炼厂可以更加高效的加工厂金属至铝矿,每个金属精炼厂都可以提升铝矿采集器的效率";

    string Successful = "金属精炼厂研究成功!";

    TechType techType = TechType.MetalRefinery;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币1000k 铝矿50
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1000k"),
        [ResourceType.Aluminum] = AssetsUtil.ParseNumber("50"),
    };
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
        //铝矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.AluminiumMining))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.MetalRefinery);
        facility.gameObject.SetActive(true);
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
