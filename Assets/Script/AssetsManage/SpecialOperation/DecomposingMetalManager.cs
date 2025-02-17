using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 强行分解金属棒
/// </summary>
public class DecomposingMetalManager : MonoBehaviour
{

    public string studyName { get; set; } //研究名字



    public Button btn;//按钮组件



    // Start is called before the first frame update
    void Start()
    {
        TechChecker.Instance.AddCheckMethod(Inspect);
        gameObject.SetActive(false);
        btn.onClick.AddListener(() =>
        {

            TipsManager.Instance.ShowVerify(RestartGame);

        });

    }
    /// <summary>
    /// 重启游戏
    /// </summary>
    void RestartGame()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    //检查事件
    void Inspect()
    {
        //检查金属棒
        if (TechManager.Instance.GetTechFlag(TechType.InspectWonderfulRod))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);


        }
    }








}
