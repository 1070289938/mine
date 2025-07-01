using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 飞升
/// </summary>
public class SoaringManager : MonoBehaviour
{

    public string studyName { get; set; } //研究名字


    public Button btn;//按钮组件


    SpecialPanelManager specialPanelManager;

    SpecialOptionType type = SpecialOptionType.Soaring;

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
            int count = GetHeaven();
            TipsManager.Instance.ShowVerify(RestartGame, "你确定要进行飞升吗?(这会导致游戏进入下一周目。你将会获得" + count + "飞升精华)");
        });

    }

    /// <summary>
    /// 重启游戏
    /// </summary>
    void RestartGame()
    {

        AchievementUtils.Unlock(Achievement.heaven);
        //胜利
        //显示旁白说明
        VoiceOverManager.Instance.Heaven(() =>
        {
            // 获取当前活动场景的名称
            string currentSceneName = SceneManager.GetActiveScene().name;
            // 加载当前场景
            SceneManager.LoadScene(currentSceneName);
        });
        //在显示旁白的期间重启
        ResourceManager.Instance.AddResource(ResourceType.AscensionEssence, GetHeaven(), false);

        //重生
        SaveLoadManager.Instance.SecondLife();



    }

    /// <summary>
    /// 本次获得的飞升精华
    /// </summary>
    int GetHeaven()
    {
        FacilityPanelManager facilityManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.TimeSpaceSynchronization);
        //增加20+时空同步装置重生晶体
        int count = 20;
        count += facilityManager.GetCount();
        return count;
    }



    // Update is called once per frame
    void Update()
    {

    }

    //检查事件
    void Inspect()
    {
        //嵌入四维宝石
        if (TechManager.Instance.GetTechFlag(TechType.EmbedFourDimensionalGem))
        {

            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }









}
