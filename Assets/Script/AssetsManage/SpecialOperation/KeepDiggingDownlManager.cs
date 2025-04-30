using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 继续向下挖掘
/// </summary>
public class KeepDiggingDownlManager : MonoBehaviour
{

    public string studyName { get; set; } //研究名字

    FacilityPanelManager facilityPanelManager;//深井电梯面板

    public Button btn;//按钮组件


    SpecialPanelManager specialPanelManager;

    SpecialOptionType type = SpecialOptionType.KeepDiggingDownl;

    void Awake()
    {
        specialPanelManager = GetComponent<SpecialPanelManager>();
        //初始化
        specialPanelManager.Install(type);

    }

    // Start is called before the first frame update
    void Start()
    {
        facilityPanelManager = FacilityManager.Instance.GetFacilityPanel(FacilityType.DeepShaftElevator);
        TechChecker.Instance.AddCheckMethod(Inspect);
        gameObject.SetActive(false);
        btn.onClick.AddListener(() =>
        {
            int count = GetRegeneratedCrystalCount();
            TipsManager.Instance.ShowVerify(RestartGame, "你确定要继续向下挖掘吗?(这会导致游戏进入下一周目。你将会获得" + count + "重生晶体)");
        });

    }
    /// <summary>
    /// 重启游戏
    /// </summary>
    void RestartGame()
    {

        gameObject.SetActive(false);
        //显示旁白说明星球被挖爆了
        VoiceOverManager.Instance.DiggingPlanetsLeadsRebirthOver(() =>
        {
            // 获取当前活动场景的名称
            string currentSceneName = SceneManager.GetActiveScene().name;
            // 加载当前场景
            SceneManager.LoadScene(currentSceneName);
        });

        //在显示旁白的期间重启


        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, GetRegeneratedCrystalCount(),false);

        //重生时间
        SaveLoadManager.Instance.SecondLife();



    }
    /// <summary>
    /// 获取到本次获得的重生晶体
    /// </summary>
    /// <returns></returns>
    int GetRegeneratedCrystalCount()
    {
        //增加80+(工人总数 / 20)重生晶体
        int count = 80;
        count += ResourceCountManager.Instance.GetMinerCount() / 20;
        return count;
    }


    // Update is called once per frame
    void Update()
    {

    }

    //检查事件
    void Inspect()
    {
        //深井电梯完成后
        if (facilityPanelManager.GetCount() >= 200)
        {
            // //在没有研究，研究金属棒科技的时候才会显示
            // if (!TechManager.Instance.GetTechFlag(TechType.ResearchRod))
            // {

            // }

            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }









}
