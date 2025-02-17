using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//水泥生产的研究
public class CementManufactureStudy : MonoBehaviour
{


    string studyName = "学习水泥的生产";

    string details = "据说用石头可以制作成水泥！\n\n可以在挖矿界面招募水泥工人";

    string Successful = "水泥生产研究成功!";

    TechType techType = TechType.CementManufacture;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Stone] = 1000;
        resources[ResourceType.Currency] = 3000;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //软妹币
        //石矿工人
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.Currency) &&
        TechManager.Instance.GetTechFlag(TechType.StoneMiner))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.ConcreteBuilding);//水泥房屋
            TechChecker.Instance.AddCheckTech(TechType.StrengthenWarehouse);//加强仓库
            TechChecker.Instance.AddCheckTech(TechType.painted);//画饼

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //解锁水泥工人 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.CementWorker);
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
