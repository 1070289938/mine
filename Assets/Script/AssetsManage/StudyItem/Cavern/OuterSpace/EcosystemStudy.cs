using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//零重力制造业
public class EcosystemStudy : MonoBehaviour
{


    string studyName = "太空生态系统";

    string details = "研究在太空中完成生态循环,从而降低人造卫星的制造成本\n\n人造卫星的成本减少15%";

    string Successful = "太空生态系统研究成功";

    TechType techType = TechType.ecosystem;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("655M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("275K"),

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
        //人造卫星
        if (TechManager.Instance.GetTechFlag(TechType.ArtificialSatellite))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {


        //获取到人造卫星
        FacilityPanelManager facilityPanelManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.ArtificialSatellite);

        facilityPanelManager.AddUpMultiple(0.15);

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
