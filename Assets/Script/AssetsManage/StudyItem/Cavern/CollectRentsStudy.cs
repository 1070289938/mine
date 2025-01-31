using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//收租的研究
public class CollectRentsStudy : MonoBehaviour
{
   

    string studyName = "收工人的房租";

    string details = "让工人住进屋子并且收他们租金总体提升房屋软妹币的产量";

    string Successful = "收租研究成功!";

    TechType techType = TechType.collectRents;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency]=500;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName,details,resources,techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study=Study;
    }
    
    void Inspect(){
        //房屋
        //软妹币
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Currency)&&
             TechManager.Instance.GetTechFlag(TechType.house)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.SaveMoney);//储钱罐
        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
       
        
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
