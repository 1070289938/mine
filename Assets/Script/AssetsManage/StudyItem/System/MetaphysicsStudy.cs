using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//玄学的研究
public class MetaphysicsStudy : MonoBehaviour
{


    string studyName = "玄学";

    string details = "提升100%最大科技点储量";

    string Successful = "玄学研究成功!";

    TechType techType = TechType.Metaphysics;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 5000,
        [ResourceType.DimensionalStone] = 800,
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

        if (TechManager.Instance.GetTechFlag(TechType.BornScientist))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {


       
        ResourceAdditionManager.Instance.AddTechnological(1);

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
