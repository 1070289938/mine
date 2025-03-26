using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//解析语言
public class AnalyticLanguageStudy : MonoBehaviour
{


    string studyName = "解析语言";

    string details = "你认为这个立碑是为了警示来到这个地方的人而立起来的,看起来十分的重要,你决定召集科学家们来解析一下他们的语言";

    string Successful = "具立碑中所说,这片区域有一座大门,他在警示后人千万不要打开这个大门";

    TechType techType = TechType.AnalyticLanguage;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("35M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("35K"),

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
        //探索痕迹
        if (TechManager.Instance.GetTechFlag(TechType.trace))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
      
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
