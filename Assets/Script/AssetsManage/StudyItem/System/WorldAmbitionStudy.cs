using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//世界级野心的研究
public class WorldAmbitionStudy : MonoBehaviour
{


    string studyName = "世界级野心";

    string details = "提升软妹币100%总产量";

    string Successful = "世界级野心研究成功!";

    TechType techType = TechType.WorldAmbition;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 5000,
        [ResourceType.DimensionalStone] = 600,
        [ResourceType.AscensionEssence] = 50,
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
        //需要重生水晶
        if (TechManager.Instance.GetTechFlag(TechType.UnscrupulousEnterprise))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {

        ResourceAdditionManager.Instance.AddRMBboost(1);

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
