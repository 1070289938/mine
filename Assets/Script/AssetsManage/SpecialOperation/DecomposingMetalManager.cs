using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        gameObject.SetActive(false);
        //显示旁白说明矿洞塌陷
        VoiceOverManager.Instance.DecomposingRestVoiceOver(() =>
        {
            // 获取当前活动场景的名称
            string currentSceneName = SceneManager.GetActiveScene().name;
            // 加载当前场景
            SceneManager.LoadScene(currentSceneName);
        });

        //在显示旁白的期间重启
        //固定增加50重生晶体
        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, 50);

        //先清空所有的科技
        SaveLoadManager.Instance.SecondLife();

       

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
