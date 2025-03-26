using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//熔岩防护服
public class LavaSuitStudy : MonoBehaviour
{


    string studyName = "熔岩防护服";

    string details = "熔岩防护服专门为地心而设计,可以耐受极高的温度,甚至可以在岩浆里洗澡！";

    string Successful = "熔岩防护服研究成功";

    TechType techType = TechType.LavaProtectiveSuit;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("15M"),
        [ResourceType.Science] = 1000,
        [ResourceType.Alloy] = 1000,

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
        //防护服
        if (TechManager.Instance.GetTechFlag(TechType.ProtectiveSuit))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

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
