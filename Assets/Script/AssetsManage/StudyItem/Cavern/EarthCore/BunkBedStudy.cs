using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//双层床
public class BunkBedStudy : MonoBehaviour
{


    string studyName = "双层床";

    string details = "双层床可以提升兵营的士兵储量\n\n每个兵营的士兵储量额外+1";

    string Successful = "双层床研究成功";

    TechType techType = TechType.BunkBed;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("35M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("15K"),
        [ResourceType.silver] = AssetsUtil.ParseNumber("8M"),

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
        //兵营
        if (TechManager.Instance.GetTechFlag(TechType.Barracks))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        BarracksManager barracksManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.Barracks).GetComponent<BarracksManager>();
        barracksManager.AddCount(1);
        
        BattlePanelManager.Instance.UpdateSoldierMax();
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
