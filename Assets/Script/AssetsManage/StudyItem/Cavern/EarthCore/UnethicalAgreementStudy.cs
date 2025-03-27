using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//不道德协议
public class UnethicalAgreementStudy : MonoBehaviour
{


    string studyName = "不道德协议";

    string details = "入伍者在合同副本15米范围内停留一秒将会视为已读并且同意入伍\n\n新兵训练营的效率提升100%";

    string Successful = "不道德协议研究成功";

    TechType techType = TechType.UnethicalAgreement;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("85M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("50K"),

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
        //七天速成班
        if (TechManager.Instance.GetTechFlag(TechType.SevenDaysCrashCourse))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //训练营效率提升
        ResourceAdditionManager.Instance.AddTrainingCampEfficiency(1);
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
