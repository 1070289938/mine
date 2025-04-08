using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//寺庙的研究
public class TempleStudy : MonoBehaviour
{


    string studyName = "寺庙";

    string details = "有信仰才会有动力!每个寺庙都可以小幅提升所有产量\n\n解锁寺庙";

    string Successful = "寺庙研究成功!";

    TechType techType = TechType.Temple;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency] = 5000;
        resources[ResourceType.Stone] = 1500;

        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //水泥生产
        //信仰
        if (TechManager.Instance.GetTechFlag(TechType.CementManufacture) &&
        TechManager.Instance.GetTechFlag(TechType.Faith))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);



        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //解锁寺庙 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Temple);
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
