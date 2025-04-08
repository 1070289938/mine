using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//军训宿舍
public class BarracksQuartersStudy : MonoBehaviour
{


    string studyName = "军营宿舍";

    string details = "在兵营里面安排宿舍供更多的士兵居住\n\n每个兵营的士兵储量额外+2";

    string Successful = "军营宿舍研究成功";

    TechType techType = TechType.BarracksQuarters;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("85M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("65K"),
        [ResourceType.Tungsten] = AssetsUtil.ParseNumber("5000"),

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
        //双重床
        if (TechManager.Instance.GetTechFlag(TechType.BunkBed))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       BarracksManager barracksManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.Barracks).GetComponent<BarracksManager>();
       barracksManager.AddCount(2);
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
