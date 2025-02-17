using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//炼制钢铁的研究
public class RefinedIronSteelStudy : MonoBehaviour
{


    string studyName = "炼制钢铁";

    string details = "把铁重新加工一下,可以获取到一个新的坚硬材料“钢!”\n\n解锁钢铁铸造工";

    string Successful = "炼制钢铁研究成功!";

    TechType techType = TechType.RefinedIronSteel;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币10k ，煤矿1000，铁矿3000
        [ResourceType.Currency] = 10000,
        [ResourceType.Colliery] = 1000,
        [ResourceType.Iron] = 3000,
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
        //挖掘煤炭
        if (TechManager.Instance.GetTechFlag(TechType.DigCoal))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.IronSteelFoundry);
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
