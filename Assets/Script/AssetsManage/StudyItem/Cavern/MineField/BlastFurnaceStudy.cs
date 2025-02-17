using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//高炉的研究
public class BlastFurnaceStudy : MonoBehaviour
{


    string studyName = "高炉";

    string details = "高炉可以快速的熔炼铁矿,大幅度的提升铁矿的生产速度\n\n解锁高炉";

    string Successful = "高炉研究成功!";

    TechType techType = TechType.BlastFurnace;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币5000 ，煤900. 钢230
        [ResourceType.Currency] = 5000,
        [ResourceType.Colliery] = 900,
        [ResourceType.Steel] = 230,
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
        //炼制钢铁
        if (TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {

        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.BlastFurnace);
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
