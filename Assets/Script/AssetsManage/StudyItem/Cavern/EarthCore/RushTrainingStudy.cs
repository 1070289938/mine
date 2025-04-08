using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//加急训练
public class RushTrainingStudy : MonoBehaviour
{


    string studyName = "加急训练";

    string details = "缩短训练时间,加强训练难度大幅提升新兵训练营的效率\n\n新兵训练营的效率提升40%";

    string Successful = "加急训练研究成功";

    TechType techType = TechType.RushTraining;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("90M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("68K"),

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
        //广泛招生
        if (TechManager.Instance.GetTechFlag(TechType.WideEnrollment))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {   
        //训练营效率提升
        ResourceAdditionManager.Instance.AddTrainingCampEfficiency(0.4);
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
