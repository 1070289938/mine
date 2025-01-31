using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//矿灯的研究
public class MinerlampStudy : MonoBehaviour
{
   

    string studyName = "矿灯";

    string details = "用铜矿与铁的结合可以制作矿灯\n\n提升20%采矿工人的效率";

    string Successful = "矿灯研究成功!";

    TechType techType = TechType.minerlamp;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency]=1000;
        resources[ResourceType.Copper]=200;
        resources[ResourceType.Iron]=100;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName,details,resources,techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study=Study;
    }
    
    void Inspect(){
        //如果有铜+铁就解锁这个科技
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Copper)&&
            ResourceManager.Instance.IsResourceUnlocked(ResourceType.Iron)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //提升采矿工人20%效率
        ResourceAdditionManager.Instance.AddMiningWorker(0.2);
        
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
