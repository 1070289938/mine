using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//热武器
public class ThermalWeaponStudy : MonoBehaviour
{


    string studyName = "热武器";

    string details = "使用热武器来与敌人战斗\n\n提升士兵50%战斗力";

    string Successful = "热武器研究成功";

    TechType techType = TechType.ThermalWeapon;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("35M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("30k"),
        [ResourceType.Tungsten] = AssetsUtil.ParseNumber("5K"),

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
        //冷兵器
        if (TechManager.Instance.GetTechFlag(TechType.ColdWeapon))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        ResourceAdditionManager.Instance.AddCombatPower(0.5);
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
