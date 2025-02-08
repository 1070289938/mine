using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//破开石头的研究
public class BrokenStoneStudy : MonoBehaviour
{


    string studyName = "防止洞口再次坍塌";

    string details = "你发现这个厚重的石墙里面冒出了一丝丝的光芒,里面肯定有一片新的世界！你需要制造资源来防止再次坍塌";

    string Successful = "洞口开启成功!";

    TechType techType = TechType.BrokenStone;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency] = AssetsUtil.ParseNumber("20k");
        resources[ResourceType.Cement] = 1000;
        resources[ResourceType.Iron] = 5000;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //大锤
        //大饼
        if (TechManager.Instance.GetTechFlag(TechType.tupid) &&
        TechManager.Instance.GetTechFlag(TechType.painted))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
            if (!TechManager.Instance.GetTechFlag(TechType.FindLight))
            {
                LogManager.Instance.AddLog("有人在石壁上发现了一丝太阳照射的光芒！！");
                TechManager.Instance.techTypeStudyFlag[TechType.FindLight] = true;
            }

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //解锁仓库 建筑按钮

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
