using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//零重力实验室
public class ZeroGravityLaboratoryStudy : MonoBehaviour
{


    string studyName = "零重力实验室";

    string details = "在太空电梯的顶部建立一个零重力实验室\n\n科技点产量提升20%";

    string Successful = "零重力实验室研究成功";

    TechType techType = TechType.ZeroGravityLaboratory;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("595M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("255K"),

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
        //人造卫星
        if (TechManager.Instance.GetTechFlag(TechType.ArtificialSatellite))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
      
        ResourceAdditionManager.Instance.AddScience(0.2);

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
