using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//企业家的研究
public class EntrepreneurStudy : MonoBehaviour
{


    string studyName = "企业家";

    string details = "提升软妹币50%总产量";

    string Successful = "商业头脑研究成功!";

    TechType techType = TechType.Entrepreneur;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 999,
        [ResourceType.DimensionalStone] = 80,
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
        //需要四维宝石
        //商业头脑
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.DimensionalStone) &&
        TechManager.Instance.GetTechFlag(TechType.BusinessAcumen))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {

        ResourceAdditionManager.Instance.AddRMBboost(0.5);

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
