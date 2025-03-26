using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//白银甲胄
public class SilverArmourStudy : MonoBehaviour
{


    string studyName = "白银甲胄";

    string details = "在防护服上裹上白银,让其更加坚固\n\n提升士兵30%战斗力";

    string Successful = "白银甲胄研究成功";

    TechType techType = TechType.SilverArmour;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("35M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("30k"),
        [ResourceType.silver] = AssetsUtil.ParseNumber("6M"),

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
        //兵营
        if (TechManager.Instance.GetTechFlag(TechType.Barracks))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        ResourceAdditionManager.Instance.AddCombatPower(0.3);
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
