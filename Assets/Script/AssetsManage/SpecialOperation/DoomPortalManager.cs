using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 毁灭传送门
/// </summary>
public class DoomPortalManager : MonoBehaviour
{

    public string studyName { get; set; } //研究名字


    public Button btn;//按钮组件


    SpecialPanelManager specialPanelManager;

    SpecialOptionType type = SpecialOptionType.DoomPortal;

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
            //如果小于250k就是战斗失败
            if (power < 250000)
            {

                TipsManager.Instance.ShowVerify(RestartGame, "你确定要进军去毁灭传送门吗?(这会导致游戏进入下一周目。你将会获得" + count + "重生晶体)");
            }
            else
            {

                TipsManager.Instance.ShowVerify(RestartGame, "你确定要进军去毁灭传送门吗?(这会导致游戏进入下一周目。你将会获得" + count + "重生晶体与50四维宝石)");
            }


        });

    }

    /// <summary>
    /// 重启游戏
    /// </summary>
    void RestartGame()
    {
        int power = BattlePanelManager.Instance.GetPower();
        gameObject.SetActive(false);

        if (power >= 250000)
        {
            //胜利
            //显示旁白说明
            VoiceOverManager.Instance.DestroyPortalWinVoiceOver(() =>
            {
                // 获取当前活动场景的名称
                string currentSceneName = SceneManager.GetActiveScene().name;
                // 加载当前场景
                SceneManager.LoadScene(currentSceneName);
            });
            //在显示旁白的期间重启
            ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, GetRegeneratedCrystalCount(), false);
            ResourceManager.Instance.AddResource(ResourceType.DimensionalStone, 50, false);
        }
        else
        {   //失败
            //显示旁白说明
            VoiceOverManager.Instance.DestroyPortalLossVoiceOver(() =>
            {
                // 获取当前活动场景的名称
                string currentSceneName = SceneManager.GetActiveScene().name;
                // 加载当前场景
                SceneManager.LoadScene(currentSceneName);
            });

            //在显示旁白的期间重启
            ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, GetRegeneratedCrystalCount(), false);


        }

        //重生
        SaveLoadManager.Instance.SecondLife();



    }
    /// <summary>
    /// 获取到本次获得的重生晶体
    /// </summary>
    /// <returns></returns>
    int GetRegeneratedCrystalCount()
    {
        //增加100+(工人总数 / 20)+（战斗力/ 1000）重生晶体
        int count = 100;
        count += ResourceCountManager.Instance.GetMinerCount() / 20;
        int power = BattlePanelManager.Instance.GetPower() / 1000;
        count += power > 250 ? 250 : power;
        return count;
    }


    // Update is called once per frame
    void Update()
    {

    }

    //检查事件
    void Inspect()
    {
        //派遣侦察队
        if (TechManager.Instance.GetTechFlag(TechType.DispatchReconnaissanceParty))
        {

            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }









}
