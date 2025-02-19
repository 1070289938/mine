using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//石镐的研究
public class CopperWarehouseStudy : MonoBehaviour
{
   

    string studyName = "铜制仓库";

    string details = "用铜搭建的仓库好像更加的实用和好看\n\n提升30%仓库储量上限";

    string Successful = "铜制仓库研究成功!";

    TechType techType = TechType.CopperWarehouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Stone]=400;
        resources[ResourceType.Copper]=80;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName,details,resources,techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study=Study;
    }
    
    void Inspect(){
        //铜矿
        //仓库
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Copper)&&
             TechManager.Instance.GetTechFlag(TechType.warehouse)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            
        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件

        //提升仓库30%储量上限
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);

        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(0.3);
        //需求材料增加铜矿
        facilityPanel.AddOnClickedResource(ResourceType.Copper,1);

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
