using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钨制甲胄
public class TungstenArmorStudy : MonoBehaviour
{


    string studyName = "钨制甲胄";

    string details = "用钨来强化防护服,让其更加坚固\n\n提升士兵35%战斗力";

    string Successful = "钨制甲胄研究成功";

    TechType techType = TechType.TungstenArmor;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("45M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("39k"),
        [ResourceType.Tungsten] = AssetsUtil.ParseNumber("5k"),

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
        //白银甲胄
        if (TechManager.Instance.GetTechFlag(TechType.SilverArmour))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        ResourceAdditionManager.Instance.AddCombatPower(0.35);
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
