using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//昔日的记忆的研究
public class PastMemoryStudy : MonoBehaviour
{


    string studyName = "昔日的记忆";

    string details = "所有建筑的资源消耗增长减少3%";

    string Successful = "昔日的记忆研究成功!";

    TechType techType = TechType.PastMemory;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 250
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
        //前世的回忆
        if (TechManager.Instance.GetTechFlag(TechType.MemoriesPastLives))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {

        //获取到所有的建筑
        FacilityPanelManager[] enemies = FindObjectsByType<FacilityPanelManager>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        //减少所有建筑1%的资源增长
        foreach (FacilityPanelManager panel in enemies)
        {
            panel.AddUpMultiple(0.03);
        }


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
