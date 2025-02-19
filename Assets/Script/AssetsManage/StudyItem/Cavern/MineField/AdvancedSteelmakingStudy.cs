using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//高级炼钢术的研究
public class AdvancedSteelmakingStudy : MonoBehaviour
{


    string studyName = "高级炼钢术";

    string details = "一种新型的炼钢方法,可以更加快速的将铁炼制成钢\n\n提升钢100%生产速度";

    string Successful = "高级炼钢术研究成功!";

    TechType techType = TechType.AdvancedSteelmaking;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币150k ，钢1000
        [ResourceType.Currency] = AssetsUtil.ParseNumber("150k"),
        [ResourceType.Steel] = 1000,
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
        //高炉
        if (TechManager.Instance.GetTechFlag(TechType.BlastFurnace))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddSteel(1);
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
