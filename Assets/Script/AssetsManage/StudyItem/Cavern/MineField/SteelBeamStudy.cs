using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钢梁的研究
public class SteelBeamStudy : MonoBehaviour
{


    string studyName = "钢梁";

    string details = "在房屋的结构上加上钢梁,可以大幅度的减少建筑成本\n\n房屋的建筑资源增长-10%";

    string Successful = "钢梁研究成功!";

    TechType techType = TechType.SteelBeam;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new()
    {
        //设置价格 软妹币60k ，钢 200
        [ResourceType.Currency] = AssetsUtil.ParseNumber("60k"),
        [ResourceType.Steel] = 200,
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
        //钢
        //支撑柱
        if (TechManager.Instance.GetTechFlag(TechType.shore) &&
        TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel))
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
