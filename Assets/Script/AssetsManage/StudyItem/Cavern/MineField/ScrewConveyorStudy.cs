using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//螺旋输送机的研究
public class ScrewConveyorStudy : MonoBehaviour
{


    string studyName = "螺旋输送机";

    string details = "这种大型机器可以使水泥工人更快的搅拌水泥,可以大幅度的提升水泥的生产速度\n\n水泥的生产速度提升150%";

    string Successful = "螺旋输送机研究成功!";

    TechType techType = TechType.ScrewConveyor;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币5000 ，铁矿1000
        [ResourceType.Currency] = 5000,
        [ResourceType.Iron] = 1000,
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
        //大型机器
        if (TechManager.Instance.GetTechFlag(TechType.LargeMachine))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddCement(1.5);
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
