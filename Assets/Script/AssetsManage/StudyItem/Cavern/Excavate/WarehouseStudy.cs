using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//仓库的研究
public class WarehouseStudy : MonoBehaviour
{
   

    string studyName = "搭建仓库";

    string details = "用大量的石头搭成一片可储存空间\n\n可以用石头建造仓库,提升资源储量上限";

    string Successful = "仓库研究成功!";

    TechType techType = TechType.warehouse;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Stone]=20;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName,details,resources,techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study=Study;
    }
    
    void Inspect(){

        Debug.Log("监听仓库");
        //如果有石头就解锁这个科技
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Stone)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
            TechChecker.Instance.AddCheckTech(TechType.CopperWarehouse);//铜制仓库
        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件
        //解锁仓库 建筑按钮
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);
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
