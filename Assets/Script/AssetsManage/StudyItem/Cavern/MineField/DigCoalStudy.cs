using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//煤炭挖掘的研究
public class DigCoalStudy : MonoBehaviour
{


    string studyName = "煤炭挖掘";

    string details = "研究一下挖掘煤炭的工具,这样就可以去挖煤啦\n\n解锁煤炭工人";

    string Successful = "煤炭挖掘研究成功!";

    TechType techType = TechType.DigCoal;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币25k ，铁矿1000
        [ResourceType.Currency] = AssetsUtil.ParseNumber("25k"),
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
        //矿物筛选器
        if (TechManager.Instance.GetTechFlag(TechType.MineralFilter))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        //解锁铜矿工人 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.CoalWorker);
        facility.gameObject.SetActive(true);
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
