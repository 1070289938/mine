using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//火星殖民地
public class MarsColonyStudy : MonoBehaviour
{


    string studyName = "火星殖民地";

    string details = "火星距离我们的家乡十分的遥远,必须要有殖民地的支持才可以进行发展\n解锁火星殖民地";

    string Successful = "火星殖民地研究成功";

    TechType techType = TechType.MarsColony;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("400M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("180K"),
        [ResourceType.Mithril] = AssetsUtil.ParseNumber("2000"),
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
        //火星殖民船
        if (TechManager.Instance.GetTechFlag(TechType.MarsColonyShip))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.MarsColony);
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
