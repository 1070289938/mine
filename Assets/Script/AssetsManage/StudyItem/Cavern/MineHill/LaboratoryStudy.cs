using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//实验室的研究
public class LaboratoryStudy : MonoBehaviour
{


    string studyName = "实验室";

    string details = "你对这个神奇的金属棒耿耿于怀,发现这个世界远远没有想象中的那么简单,所以你准备召集科学家来研究这个神奇的东西\n\n解锁实验室";

    string Successful = "实验室研究成功!";

    TechType techType = TechType.Laboratory;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币2000k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1000k"),
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
        //工厂建设
        if (TechManager.Instance.GetTechFlag(TechType.PlantConstruction))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Laboratory);
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
