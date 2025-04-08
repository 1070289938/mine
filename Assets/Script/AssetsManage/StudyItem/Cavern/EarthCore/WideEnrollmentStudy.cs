using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//广泛招生
public class WideEnrollmentStudy : MonoBehaviour
{


    string studyName = "广泛招生";

    string details = "大面积的进行征兵,提升新兵训练营的效率\n\n新兵训练营的效率提升35%";

    string Successful = "广泛招生研究成功";

    TechType techType = TechType.WideEnrollment;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("48M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("40K"),

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
        //新兵训练营
        if (TechManager.Instance.GetTechFlag(TechType.BootCamp))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //训练营效率提升
        ResourceAdditionManager.Instance.AddTrainingCampEfficiency(0.35);
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
