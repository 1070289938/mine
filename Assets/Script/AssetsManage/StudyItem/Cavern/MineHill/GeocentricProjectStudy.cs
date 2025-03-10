using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//工厂建设的研究
public class GeocentricProjectStudy : MonoBehaviour
{


    string studyName = "地心计划";

    string details = "为了挖掘更加坚硬的稀有矿物,你决定施行地心计划,使用盾构机向地底打洞,并且建造深井电梯来到达地心采矿";

    string Successful = "地心计划研究成功!";

    TechType techType = TechType.GeocentricProject;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币8000k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("8000k"),
    };
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
        //盾构机
        if (TechManager.Instance.GetTechFlag(TechType.ShieldTunnelingMachine))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        
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
