using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//研究项目
public class StudyItemManager : MonoBehaviour
{

    public string studyName { get; set; } //研究名字

    public string details { get; set; } //研究详情

    public TechType techType { get; set; } //类型

    public Dictionary<ResourceType, double> resources { get; set; } //研究需要的资源
    
    public Action Inspect;//检查事件
   
    public Action Study; //研究事件
    public Button btn;//按钮组件




    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //初始化按钮
    public void Install(string studyName,string details,Dictionary<ResourceType, double> resources,TechType techType){
        this.studyName = studyName;
        this.details = details;
        this.resources = resources;
        this.techType =techType;
    }

    //显示详情
    public void ShowItemMsg(){
        if(TipsManager.Instance!=null){
            TipsManager.Instance.ShowResearchProject(this);
        }
       
    }


    public JudgmentResult StudyFrame(){
    JudgmentResult result = ResourceManager.Instance.JudgmentResource(resources);
       if(!result.flag){
            //资源不足！，对应的资源进行抖动
            return result;
       }
        //设置为已研究
        TechManager.Instance.techTypeStudyFlag[techType] =true;
        gameObject.SetActive(false);
        Study.Invoke();
        return result;
    }



}

