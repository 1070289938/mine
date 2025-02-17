using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanelManager : MonoBehaviour
{
    public GameObject miningPanel;  // 挖矿界面
    public GameObject researchPanel;  // 研究界面

    public GameObject SpecialPanel;  // 特殊界面

    public GameObject SystemPanel;  // 系统界面


    public static bool ResearchRedHint;//红色提示是否显示

    public GameObject researchHit;//研究提示
    int show = 10;

    int hide = 0;

    // 显示挖矿界面
    public void ShowMiningPanel()
    {
        miningPanel.transform.SetSiblingIndex(show);
        researchPanel.transform.SetSiblingIndex(hide);
        SpecialPanel.transform.SetSiblingIndex(hide);
        SystemPanel.transform.SetSiblingIndex(hide);
    }

    // 显示研究界面
    public void ShowResearchPanel()
    {
        SpecialPanel.transform.SetSiblingIndex(hide);
        SystemPanel.transform.SetSiblingIndex(hide);
        miningPanel.transform.SetSiblingIndex(hide);
        researchPanel.transform.SetSiblingIndex(show);
    }

    //显示特殊界面

    public void ShowSpecialPanel()
    {
        SpecialPanel.transform.SetSiblingIndex(show);
        SystemPanel.transform.SetSiblingIndex(hide);
        miningPanel.transform.SetSiblingIndex(hide);
        researchPanel.transform.SetSiblingIndex(hide);
    }


    //显示系统界面

    public void ShowSystemPanel()
    {
        SpecialPanel.transform.SetSiblingIndex(hide);
        SystemPanel.transform.SetSiblingIndex(show);
        miningPanel.transform.SetSiblingIndex(hide);
        researchPanel.transform.SetSiblingIndex(hide);
    }

    void Update()
    {
        // ResearchPanelMonitor();
    }
    // /// <summary>
    // /// 研究红点显示
    // /// </summary>
    // void ResearchPanelMonitor()
    // {
    //     if (ResearchRedHint)
    //     {
    //         //显示提示
    //         researchHit.SetActive(true);
    //     }
    //     else
    //     {
    //         //隐藏提示
    //         researchHit.SetActive(false);
    //     }

    // }


}
