using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipsManager : MonoBehaviour
{
    public GameObject black;

    public GameObject researchProject;

    public VerifyMsgManager verifyMsgManager;


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

    // Update is called once per frame
    void Update()
    {

    }
    //背景被点击
    void OnclickBackgroud()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }


    //打开研究项目详情
    public void ShowResearchProject(StudyItemManager studyItem)
    {
        //显示黑色背景和研究项目
        researchProject.SetActive(true);
        black.SetActive(true);
        //重新排列研究项目的内容

        researchProject.GetComponent<ItemMsgManager>().Install(studyItem);


    }

    //强行分解金属棒的确认
    public void ShowVerify(Action action)
    {
         //显示黑色背景和研究项目
        verifyMsgManager.gameObject.SetActive(true);
        black.SetActive(true);
        //重新排列研究项目的内容
        verifyMsgManager.Install("你确定要强行分解金属棒吗?(这会导致游戏进入下一周目。你将会获得50重生晶体)",action);
    }


}
