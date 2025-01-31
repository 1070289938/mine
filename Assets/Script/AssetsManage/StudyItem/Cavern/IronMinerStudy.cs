using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铁矿工人的研究
public class IronMinerStudy : MonoBehaviour
{
   

    string studyName = "招聘铁矿工人";

    string details = "与铜矿工人一样,他们只会在矿堆里挑有铁矿的地方挖,所以效率并不是很高，但是总比乱挖的要快吧！\n\n可以在挖矿界面招募铁矿工人";

    string Successful = "铁矿工人研究成功!";

    TechType techType = TechType.IronMiner;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency]=500;
        resources[ResourceType.Iron]=10;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName,details,resources,techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study=Study;
    }
    
    void Inspect(){
        //软妹币
        //铁
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Currency)&&
            ResourceManager.Instance.IsResourceUnlocked(ResourceType.Iron)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //解锁铁矿工人 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.IronMiner);
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
