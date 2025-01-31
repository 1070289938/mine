using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//房屋的研究
public class HouseStudy : MonoBehaviour
{
   

    string studyName = "搭建房屋";

    string details = "用大量的石头搭成一栋可居住房屋\n\n工人们可以居住房屋来产生软妹币";

    string Successful = "房屋研究成功!";

    TechType techType = TechType.house;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
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
        //如果有石头就解锁这个科技
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Stone)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.collectRents);//收租

        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //解锁房屋 建筑按钮
         FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.Tenement);
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
