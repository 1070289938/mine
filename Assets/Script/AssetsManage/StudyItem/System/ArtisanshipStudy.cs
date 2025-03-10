using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//技工天赋的研究
public class ArtisanshipStudy : MonoBehaviour
{


    string studyName = "技工天赋";

    string details = "制造工人的效率提升100%";

    string Successful = "技工天赋研究成功!";

    TechType techType = TechType.Artisanship;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 45
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
        //重生晶体
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.RegeneratedCrystal))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {

        //提升100%制造工人效率
        ResourceAdditionManager.Instance.AddFabricator(1);


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
