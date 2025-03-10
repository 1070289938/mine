using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechManager : MonoBehaviour
{
    public static TechManager Instance { get; private set; }

    public GameObject content;//Tech科技
    public GameObject SystemContent;//系统科技

    public GameObject Special;//特殊科技

    public Dictionary<TechType, StudyItemManager> techTypeDictionary = new Dictionary<TechType, StudyItemManager>();//科技的管理层

    public Dictionary<SpecialOptionType, SpecialPanelManager> specialPanelManagerMap = new();//特殊科技的管理层


    public Dictionary<TechType, bool> techTypeStudyFlag;//科技是否研究


    void Awake()
    {
        Instance = this;
        ///特殊科技
        SpecialPanelManager[] SpecialStudyItems = Special.GetComponentsInChildren<SpecialPanelManager>(true);
        foreach (SpecialPanelManager itemManager in SpecialStudyItems)
        {
            specialPanelManagerMap[itemManager.specialOptionType] = itemManager;
        }
    }

    private void Start()
    {
        ///普通科技
        StudyItemManager[] studyItems = content.GetComponentsInChildren<StudyItemManager>(true);
        if (techTypeStudyFlag == null)
        {
            techTypeStudyFlag = new Dictionary<TechType, bool>();
        }
        foreach (StudyItemManager itemManager in studyItems)
        {
            techTypeDictionary[itemManager.techType] = itemManager;
            TechChecker.Instance.AddCheckMethod(itemManager.InspectFrame);//直接监听所有科技
        }

        ///系统科技
        StudyItemManager[] systemStudyItems = SystemContent.GetComponentsInChildren<StudyItemManager>(true);
        foreach (StudyItemManager itemManager in systemStudyItems)
        {
            techTypeDictionary[itemManager.techType] = itemManager;
            TechChecker.Instance.AddCheckMethod(itemManager.InspectFrame);//直接监听所有科技
        }



    }

    // 获取科技脚本实例
    public StudyItemManager GetTech(TechType techType)
    {
        return techTypeDictionary[techType];
    }


    // 获取特殊科技实例
    public SpecialPanelManager GetSpecialTech(SpecialOptionType techType)
    {
        return specialPanelManagerMap[techType];
    }




    // 获取科技是否研究
    public bool GetTechFlag(TechType techType)
    {
        if (techTypeStudyFlag.ContainsKey(techType))
        {
            return techTypeStudyFlag[techType];
        }
        return false;
    }
}