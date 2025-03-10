using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//注重细节的研究
public class AttentionDetailStudy : MonoBehaviour
{


    string studyName = "注重细节";

    string details = "制造工人的效率提升50%";

    string Successful = "注重细节研究成功!";

    TechType techType = TechType.AttentionDetail;
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
        //技工天赋
        if (TechManager.Instance.GetTechFlag(TechType.Artisanship))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {

        //提升50%制造工人效率
        ResourceAdditionManager.Instance.AddFabricator(0.5);


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
