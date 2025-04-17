using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//智能仓储
public class IntelligentStorageStudy : MonoBehaviour
{


    string studyName = "智能仓储";

    string details = "将仓库接入AI,AI会不停的自动进行资源的分类以提高容量\n\n仓库储量提升100%";

    string Successful = "智能仓储研究成功";

    TechType techType = TechType.IntelligentStorage;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("4.5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("425K"),
        [ResourceType.Mithril] = AssetsUtil.ParseNumber("3000"),

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
        //AI
        if (TechManager.Instance.GetTechFlag(TechType.AI))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

          //触发研究事件
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(1);

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
