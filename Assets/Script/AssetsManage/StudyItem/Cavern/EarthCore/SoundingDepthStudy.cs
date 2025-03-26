using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//探测地核深处
public class SoundingDepthStudy : MonoBehaviour
{


    string studyName = "探测地心深处";

    string details = "你听从科学家的建议从这个深度开始横向探测,看看能不能探测到空旷的地心空间";

    string Successful = "你成功的探测到了一个极大的地心空间,这里地形的环境十分苛刻，同时也有大量的稀有矿物存在";

    TechType techType = TechType.SoundingDepth;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点5k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("15M"),
        [ResourceType.Science] = 5000,

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
        //到达地心
        if (TechManager.Instance.GetTechFlag(TechType.ReachCore))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

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
