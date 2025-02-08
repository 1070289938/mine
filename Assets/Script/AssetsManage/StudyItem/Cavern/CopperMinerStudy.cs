using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铜矿工人的研究
public class CopperMinerStudy : MonoBehaviour
{
   

    string studyName = "招聘铜矿工人";

    string details = "与石矿工人不一样,他们只会在矿堆里挑有铜矿的地方挖,所以不可能比石矿工人效率高吧?\n\n可以在挖矿界面招募铜矿工人";

    string Successful = "铜矿工人研究成功!";

    TechType techType = TechType.CopperMiner;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency]=50;
        resources[ResourceType.Copper]=10;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName,details,resources,techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study=Study;
    }
    
    void Inspect(){
        //软妹币
        //铜
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Copper)&&
        ResourceManager.Instance.IsResourceUnlocked(ResourceType.Currency)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件

       
        //解锁铜矿工人 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.CopperMiner);
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
