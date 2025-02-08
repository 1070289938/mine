using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//矿车的研究
public class TramcarStudy : MonoBehaviour
{


    string studyName = "研究矿车";

    string details = "拥有铁轨了之后我们就可以让矿车在铁轨上移动啦！\n\n可以在挖矿界面制作矿车";

    string Successful = "矿车研究成功!";

    TechType techType = TechType.tramcar;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency] = 3000;
        resources[ResourceType.Iron] = 500;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

   
    public FacilityPanelManager railFacility;//铁轨管理
    void Inspect()
    {
        //铁轨搭建完毕
        if (railFacility.GetCount() >= 50)
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //解锁矿车 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.OreCar);
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
