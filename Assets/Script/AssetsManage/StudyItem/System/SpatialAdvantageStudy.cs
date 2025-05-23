using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//空间优势的研究
public class SpatialAdvantageStudy : MonoBehaviour
{


    string studyName = "空间优势";

    string details = "重生水晶提升的储量上限效果提升50%";

    string Successful = "空间优势研究成功!";

    TechType techType = TechType.SpatialAdvantage;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 150
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
        //空间储物
        if (TechManager.Instance.GetTechFlag(TechType.SpaceStorage))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {


        //提升50% 重生水晶提升的储量上限
        ResourceAdditionManager.Instance.AddRegenerateCrystalSpaceBonus(0.5);

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
