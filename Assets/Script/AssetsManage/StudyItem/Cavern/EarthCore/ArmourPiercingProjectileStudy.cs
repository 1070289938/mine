using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//穿甲弹
public class ArmourPiercingProjectileStudy : MonoBehaviour
{


    string studyName = "穿甲弹";

    string details = "穿甲弹可以直接打穿敌人坚硬的外壳来造成高额的伤害\n\n提升士兵100%战斗力";

    string Successful = "穿甲弹研究成功";

    TechType techType = TechType.ArmourPiercingProjectile;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("58M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("54k"),
        [ResourceType.Nickel] = AssetsUtil.ParseNumber("8000"),

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
        //热武器
        if (TechManager.Instance.GetTechFlag(TechType.ThermalWeapon))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        ResourceAdditionManager.Instance.AddCombatPower(1);
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
