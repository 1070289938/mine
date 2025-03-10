using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//盾构机的研究
public class ShieldTunnelingMachineStudy : MonoBehaviour
{


    string studyName = "盾构机";

    string details = "你设想出建造一个巨型的钻头直穿矿山的建筑,在你的设想中,他可以给你带来十分庞大的基础资源产量\n\n解锁盾构机工程";

    string Successful = "盾构机研究成功!";

    TechType techType = TechType.ShieldTunnelingMachine;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币4000k 合金5000
        [ResourceType.Currency] = AssetsUtil.ParseNumber("4000k"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("5000"),
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
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.ShieldTunnelingMachine);
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
