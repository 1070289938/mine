using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//多维集装箱的研究
public class DimensionalContainerStudy : MonoBehaviour
{


    string studyName = "多维集装箱";

    string details = "集装箱的效率提升50%";

    string Successful = "多维集装箱研究成功!";

    TechType techType = TechType.DimensionalContainer;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 500,
        [ResourceType.DimensionalStone] = 90
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

        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.DimensionalStone))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {


        //提升50%科技上限
        ResourceAdditionManager.Instance.AddContainer(0.5);

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
