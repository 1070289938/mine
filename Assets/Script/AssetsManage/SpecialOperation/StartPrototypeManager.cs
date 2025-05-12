using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 启动原型深渊号
/// </summary>
public class StartPrototypeManager : MonoBehaviour
{

    public string studyName { get; set; } //研究名字


    public Button btn;//按钮组件


    SpecialPanelManager specialPanelManager;

    SpecialOptionType type = SpecialOptionType.StartPrototype;

    void Awake()
    {
        specialPanelManager = GetComponent<SpecialPanelManager>();
        //初始化
        specialPanelManager.Install(type);

    }

    // Start is called before the first frame update
    void Start()
    {

        TechChecker.Instance.AddCheckMethod(Inspect);
        gameObject.SetActive(false);
        btn.onClick.AddListener(() =>
        {

            int power = BattlePanelManager.Instance.GetPower();
            int count = GetRegeneratedCrystalCount();

            TipsManager.Instance.ShowVerify(RestartGame, "你确定要将原型深渊号开入虫洞吗?(这会导致游戏进入下一周目。你将会获得" + count + "重生晶体与100四维宝石)");



        });

    }

    /// <summary>
    /// 重启游戏
    /// </summary>
    void RestartGame()
    {
        int power = BattlePanelManager.Instance.GetPower();
        gameObject.SetActive(false);

        //胜利
        //显示旁白说明
        VoiceOverManager.Instance.WormholeCollapse(() =>
        {
            // 获取当前活动场景的名称
            string currentSceneName = SceneManager.GetActiveScene().name;
            // 加载当前场景
            SceneManager.LoadScene(currentSceneName);
        });
        //在显示旁白的期间重启
        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, GetRegeneratedCrystalCount(), false);
        ResourceManager.Instance.AddResource(ResourceType.DimensionalStone, 100, false);

        //重生
        SaveLoadManager.Instance.SecondLife();



    }
    /// <summary>
    /// 获取到本次获得的重生晶体
    /// </summary>
    /// <returns></returns>
    int GetRegeneratedCrystalCount()
    {
        //增加200+(工人总数 / 20)+（战斗力/ 1000） +(火星建筑点数)+（）重生晶体
        int count = 300;
        count += ResourceCountManager.Instance.GetMinerCount() / 20;
        int power = BattlePanelManager.Instance.GetPower() / 1000;
        count += power > 250 ? 250 : power;
        count += MarsPanelManager.Instance.GetMaxCount();
        count += DeepSpacePanelManager.Instance.GetMaxCount();
        return count;
    }


    // Update is called once per frame
    void Update()
    {

    }

    //检查事件
    void Inspect()
    {
        //完成深渊号的搭建
        if (TechManager.Instance.GetTechFlag(TechType.CompleteAbyss))
        {

            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }









}
