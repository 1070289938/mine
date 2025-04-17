using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//实验型探索者
public class ExperimentalExplorerStudy : MonoBehaviour
{


    string studyName = "实验型探索者";

    string details = "科学家给我们规划出来了他的探索者制作图纸,这个飞船十分的庞大,预计需要大量的工程才可以制造出来";

    string Successful = "实验型探索者研究成功";

    TechType techType = TechType.ExperimentalExplorer;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("10G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("525K"),

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
        //完成船坞
        if (TechManager.Instance.GetTechFlag(TechType.CompleteSpaceDock))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.ExperimentalExplorer);
        facilityPanel.gameObject.SetActive(true);
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
