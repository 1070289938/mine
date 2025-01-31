using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//石矿工人的研究
public class StoneMinerStudy : MonoBehaviour
{
   

    string studyName = "招聘石矿工人";

    string details = "发布石矿工人的招聘消息,可以招募黑奴帮你一辈子免费挖石矿\n\n可以在挖矿界面招募石矿工人";

    string Successful = "石矿工人研究成功!";

    TechType techType = TechType.StoneMiner;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency]=10;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName,details,resources,techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study=Study;
    }
    
    void Inspect(){
        //软妹币
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Currency)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.StoneHammer);///石锤
        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //解锁石矿工人 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.StoneMiner);
        facility.gameObject.SetActive(true);




    }


    // Update is called once per frame
    void Update()
    {
        
    }

    //点击事件
    void onClick(){
        studyItemManager.ShowItemMsg();

    }


}
