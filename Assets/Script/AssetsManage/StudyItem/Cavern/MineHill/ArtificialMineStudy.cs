using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//人造矿井的研究
public class ArtificialMineStudy : MonoBehaviour
{


    string studyName = "人造矿井";

    string details = "设想出一个人造矿井的结构 ,他可以在距离工厂更近的地点开发矿井,从而提升所有采矿工人的效率";

    string Successful = "人造矿井研究成功!";

    TechType techType = TechType.ArtificialMine;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币6000k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("6000k"),
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
        //巨型建筑
        if (TechManager.Instance.GetTechFlag(TechType.Megastructure))
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
