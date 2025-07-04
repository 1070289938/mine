using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsManager : MonoBehaviour
{
    public GameObject black;
    public GameObject highBlack;
    public GameObject researchProject;

    public VerifyMsgManager verifyMsgManager;

    public RevenueManager revenueManager;

    public LoadManager loadManager;

    public ExchangeManager exchangeManager;

    public GameObject loadObject;

    public static TipsManager Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);

        black.GetComponent<Button>().onClick.AddListener(OnclickBackgroud);

        researchProject.GetComponent<ItemMsgManager>().off.onClick.AddListener(OnclickBackgroud);
        verifyMsgManager.off.onClick.AddListener(OnclickBackgroud);
        Instance = this;
    }



    //背景是否可点击
    private bool backgroudClickable = false;

    //背景被点击
    void OnclickBackgroud()
    {
        if (backgroudClickable)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    //关闭所有
    public void OnClickAll()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }


    //打开研究项目详情
    public void ShowResearchProject(StudyItemManager studyItem)
    {
        backgroudClickable = true;
        //显示黑色背景和研究项目
        researchProject.SetActive(true);
        black.SetActive(true);
        //重新排列研究项目的内容

        researchProject.GetComponent<ItemMsgManager>().Install(studyItem);


    }

    //关键操作的确认
    public void ShowVerify(Action action, string text)
    {

        backgroudClickable = true;
        //显示黑色背景和研究项目
        verifyMsgManager.gameObject.SetActive(true);
        black.SetActive(true);
        //重新排列研究项目的内容
        verifyMsgManager.Install(text, action);
    }

    /// <summary>
    /// 显示离线收益
    /// </summary>
    public void ShowRevenue(Dictionary<ResourceType, double> resource, int time)
    {
        backgroudClickable = false;

        //显示黑色背景和离线收益
        revenueManager.gameObject.SetActive(true);
        black.SetActive(true);

        revenueManager.Install(resource, time);

    }

    /// <summary>
    /// 显示导入存档
    /// </summary>
    public void ShowLoad()
    {
        backgroudClickable = false;
        loadManager.gameObject.SetActive(true);
        black.SetActive(true);
    }

    /// <summary>
    /// 显示兑换码
    /// </summary>
    public void ShowExchange()
    {
        backgroudClickable = false;
        exchangeManager.gameObject.SetActive(true);
        black.SetActive(true);
    }


    public void Update()
    {
        if (loadFlag)
        {
            //如果是加载中的话，记15秒，如果15秒后还没有加载完成的话就强制关闭
            loadTime += Time.deltaTime;
            if (loadTime > 15)
            {
                loadTime = 0;
                HideBackLoad();
            }
        }

    }

    /// <summary>
    /// 加载时间
    /// </summary>
    float loadTime = 0;

    bool loadFlag = false;

    //显示黑色加载框
    public void ShowBackLoad()
    {
        loadFlag = true;
        loadObject.SetActive(true);
        highBlack.SetActive(true);
    }

    public void HideBackLoad()
    {
        loadFlag = false;
        loadTime = 0;
        loadObject.SetActive(false);
        highBlack.SetActive(false);
    }



}
