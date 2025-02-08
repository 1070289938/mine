using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public string Successful { get; set; }
    public void InspectFrame()
    {
        //如果当前科技已研究或者是已显示就不管他
        if (TechManager.Instance.GetTechFlag(techType))
        {
            TechChecker.Instance.RemoveCheckMethod(Inspect);
            return;
        }
        //如果当前科技已研究或者是已显示就不管他
        if (gameObject.activeSelf)
        {
            TechChecker.Instance.RemoveCheckMethod(Inspect);
            return;
        }




        Inspect?.Invoke(); // 执行检查方法
    }


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ComputePrice();
    }

    /// <summary>
    /// 计算当前资源是否足够购买当前面板的东西
    /// </summary>
    void ComputePrice()
    {

        Dictionary<ResourceType, double> expendResources = resources;//当前价格

        JudgmentResult result = ResourceManager.Instance.OnlyJudgmentResource(expendResources);
        ColorBlock colors = btn.colors;
        Color color;
        if (result.flag)
        {
            color = Utils.HexToColor("86E37F");
        }
        else
        {
            color = Utils.HexToColor("9D9595");
        }
        colors.normalColor = color;
        colors.selectedColor = color;
        colors.pressedColor = color;
        colors.highlightedColor = color;
        btn.colors = colors;//给颜色赋值给按钮

    }

    //初始化按钮
    public void Install(string studyName, string details, Dictionary<ResourceType, double> resources, TechType techType)
    {
        this.studyName = studyName;
        this.details = details;
        this.resources = resources;
        this.techType = techType;
    }

    //显示详情
    public void ShowItemMsg()
    {
        if (TipsManager.Instance != null)
        {
            TipsManager.Instance.ShowResearchProject(this);
        }

    }


    public JudgmentResult StudyFrame()
    {
        JudgmentResult result = ResourceManager.Instance.JudgmentResource(resources);
        if (!result.flag)
        {
            //资源不足！，对应的资源进行抖动
            return result;
        }
        //设置为已研究
        TechManager.Instance.techTypeStudyFlag[techType] = true;
        gameObject.SetActive(false);
        Study.Invoke();
        LogManager.Instance.AddLog(Successful);
        return result;
    }



}

