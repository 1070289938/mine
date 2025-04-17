using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//粒子波采集
public class ParticleWaveAcquisitionStudy : MonoBehaviour
{


    string studyName = "粒子波采集";

    string details = "使用粒子波的方式采集中子,可以大幅度的提升中子的采集效率\n\n中子产量提升60%";

    string Successful = "粒子波采集研究成功";

    TechType techType = TechType.ParticleWaveAcquisition;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 655 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("4.8G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("405K"),
        [ResourceType.Neutron] = AssetsUtil.ParseNumber("120"),

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
        //中子采集器
        if (TechManager.Instance.GetTechFlag(TechType.NeutronCollector))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
       ResourceAdditionManager.Instance.AddNeutron(0.6);

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
