using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//纳米工厂
public class NanofactoryStudy : MonoBehaviour
{


    string studyName = "纳米工厂";

    string details = "纳米工厂会使用钨和镍来制造纳米材料\n\n解锁纳米工厂";

    string Successful = "纳米工厂研究成功";

    TechType techType = TechType.Nanofactory;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("21M"),
        [ResourceType.Science] = 2000,
        [ResourceType.Nickel] = AssetsUtil.ParseNumber("500"),

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
        //纳米材料
        if (TechManager.Instance.GetTechFlag(TechType.Nanomaterials))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Nanofactory);
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
