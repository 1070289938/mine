using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//激光炮塔
public class LaserTurretStudy : MonoBehaviour
{


    string studyName = "激光炮塔";

    string details = "激光炮塔完全解决了自动炮塔的不足点,但是伤害略低于自动炮塔\n\n解锁激光炮塔";

    string Successful = "激光炮塔研究成功";

    TechType techType = TechType.LaserTurret;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("23M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("30k"),
        [ResourceType.Nanomaterials] = AssetsUtil.ParseNumber("600"),
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
        //自动炮塔
        if (TechManager.Instance.GetTechFlag(TechType.AutomaticTurret))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.LaserTurret);
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
