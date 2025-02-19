using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//矿物筛选器的研究
public class MineralFilterStudy : MonoBehaviour
{


    string studyName = "矿物筛选器";

    string details = "矿物筛选器可以更加轻松的将铜矿和铁矿从石头中筛选出来\n\n解锁矿物筛选器";

    string Successful = "矿物筛选器研究成功!";

    TechType techType = TechType.MineralFilter;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币20k ，石头10k，水泥1000
        [ResourceType.Currency] =  AssetsUtil.ParseNumber("20k"),
        [ResourceType.Stone] = 10000,
        [ResourceType.Cement] = 1000,
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
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.MineralScreeningMachine);
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
