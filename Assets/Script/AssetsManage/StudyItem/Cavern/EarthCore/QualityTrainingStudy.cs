using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//素质训练
public class QualityTrainingStudy : MonoBehaviour
{


    string studyName = "素质训练";

    string details = "训练士兵来提升士兵的战斗素质\n\n提升士兵80%战斗力";

    string Successful = "素质训练研究成功";

    TechType techType = TechType.QualityTraining;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("25M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("10k"),

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
       ResourceAdditionManager.Instance.AddCombatPower(0.8);
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
