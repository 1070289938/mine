using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//月心储物
public class MoonHeartStorageStudy : MonoBehaviour
{


    string studyName = "月心储物";

    string details = "在月球的底下扩建储物空间,大幅度的提升月球物资站的效率\n\n月球物资站储物加成+200%";

    string Successful = "月心储物研究成功";

    TechType techType = TechType.MoonHeartStorage;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("950M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("225K"),

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
        //月球物资站
        if (TechManager.Instance.GetTechFlag(TechType.LunarMaterialStation))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       
        ResourceAdditionManager.Instance.AddLunarMaterialStation(2);




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
