using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//结构研究
public class StructuralStudyStudy : MonoBehaviour
{


    string studyName = "结构研究";

    string details = "研究一下古文明建筑结构来提升我们自己房屋的结构强度\n\n房屋的收入提升100%";

    string Successful = "结构研究研究成功";

    TechType techType = TechType.StructuralStudy;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 30M 科技点10k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("30M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("10K"),

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
        //地心研究所
        if (TechManager.Instance.GetTechFlag(TechType.GeocentricResearch))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddTenementComfort(1);
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
