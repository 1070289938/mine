using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//火星殖民船
public class MarsColonyShipStudy : MonoBehaviour
{


    string studyName = "火星殖民船";

    string details = "向火星派遣殖民船,占领这颗无主的星球";

    string Successful = "火星殖民船研究成功";

    TechType techType = TechType.MarsColonyShip;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1.5G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("445K"),
        [ResourceType.Mithril] = AssetsUtil.ParseNumber("1500"),

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
        //远航技术
        if (TechManager.Instance.GetTechFlag(TechType.MarsExploration))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {



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
