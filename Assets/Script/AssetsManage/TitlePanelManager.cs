using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitlePanelManager : MonoBehaviour
{
    public GameObject miningPanel;  // 挖矿界面
    public GameObject researchPanel;  // 研究界面

    int show = 2;

    int hide = 0;

    // 显示挖矿界面
    public void ShowMiningPanel()
    {
        miningPanel.transform.SetSiblingIndex(show);
        researchPanel.transform.SetSiblingIndex(hide);
    }

    // 显示研究界面
    public void ShowResearchPanel()
    {
        miningPanel.transform.SetSiblingIndex(hide);
        researchPanel.transform.SetSiblingIndex(show);
    }

}
