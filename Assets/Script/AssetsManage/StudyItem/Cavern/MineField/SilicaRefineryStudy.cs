using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//硅石精炼器的研究
public class SilicaRefineryStudy : MonoBehaviour
{


    string studyName = "硅石精炼器";

    string details = "硅石精炼器可以更加有效的对硅矿进行提取,每个硅石精炼器都可以提升硅矿的产量\n\n解锁硅石精炼器";

    string Successful = "硅石精炼器研究成功!";

    TechType techType = TechType.SilicaRefinery;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币250k ，钢300，硅50
        [ResourceType.Currency] = AssetsUtil.ParseNumber("250k"),
        [ResourceType.Steel] = 300,
        [ResourceType.Silicon] = 50,
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
        //硅石采矿机
        if (TechManager.Instance.GetTechFlag(TechType.SiliconMining))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.SiliconRefiner);
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
