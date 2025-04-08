using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//七天速成班
public class SevenDaysCrashCourseStudy : MonoBehaviour
{


    string studyName = "七天速成班";

    string details = "将新兵的训练时间压缩为七天\n\n新兵训练营的效率提升45%";

    string Successful = "七天速成班研究成功";

    TechType techType = TechType.SevenDaysCrashCourse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("108M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("80K"),

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
        //加急训练
        if (TechManager.Instance.GetTechFlag(TechType.RushTraining))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //训练营效率提升
        ResourceAdditionManager.Instance.AddTrainingCampEfficiency(0.45);
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
