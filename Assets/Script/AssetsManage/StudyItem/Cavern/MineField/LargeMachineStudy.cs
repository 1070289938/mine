using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//大型机器的研究
public class LargeMachineStudy : MonoBehaviour
{


    string studyName = "大型机器";

    string details = "矿场空旷的地形可以使用大型机器进行挖掘,大型机器可以大幅度的提升工人们的效率\n\n解锁大型机器科技分支";

    string Successful = "大型机器研究成功!";

    TechType techType = TechType.LargeMachine;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币20k ，铜矿1000
        [ResourceType.Currency] = AssetsUtil.ParseNumber("20k"),
        [ResourceType.Copper] = 1000,
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
        //破开石头
        if (TechManager.Instance.GetTechFlag(TechType.BrokenStone))
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
