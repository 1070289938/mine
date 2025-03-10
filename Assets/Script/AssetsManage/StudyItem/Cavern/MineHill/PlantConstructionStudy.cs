using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//工厂建设的研究
public class PlantConstructionStudy : MonoBehaviour
{


    string studyName = "工厂建设";

    string details = "为了更好的处理和管理这些十分庞大的资源,你决定分一块区域来建设大型工厂";

    string Successful = "工厂建设研究成功!";

    TechType techType = TechType.PlantConstruction;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币800k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("800k"),
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
        //探索山脉
        if (TechManager.Instance.GetTechFlag(TechType.ExploratoryMine))
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
