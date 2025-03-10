using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//自动化的研究
public class AutomateStudy : MonoBehaviour
{


    string studyName = "自动化";

    string details = "使所有工厂内的部分设备完成自动化\n\n提升所有工厂设备50%效率";

    string Successful = "自动化研究成功!";

    TechType techType = TechType.Automate;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币2500k 合金200
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2500k"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("200"),
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
        //合金工厂
        if (TechManager.Instance.GetTechFlag(TechType.AlloyFactory))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddStoneFactory(0.5);
        ResourceAdditionManager.Instance.AddCopperWorks(0.5);
        ResourceAdditionManager.Instance.AddCementFactory(0.5);
        ResourceAdditionManager.Instance.AddAlloyFactory(0.5);
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
