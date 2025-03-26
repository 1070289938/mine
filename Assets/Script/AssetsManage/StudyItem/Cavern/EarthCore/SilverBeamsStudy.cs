using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//银制房梁
public class SilverBeamsStudy : MonoBehaviour
{


    string studyName = "银制房梁";

    string details = "用银包裹房梁,可以使他更加的好看！\n\n房屋的建筑资源增长-10%";

    string Successful = "银制房梁研究成功";

    TechType techType = TechType.SilverBeams;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("20M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("10k"),
        [ResourceType.silver] =  AssetsUtil.ParseNumber("1M"),

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
        //银矿工人
        if (TechManager.Instance.GetTechFlag(TechType.SilverMiner))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        //房屋的建筑资源增长-10%
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);
        facility.AddUpMultiple(0.1);
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
