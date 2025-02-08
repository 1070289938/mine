using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechManager : MonoBehaviour
{
    public static TechManager Instance { get; private set; }

    public GameObject content;

    public Dictionary<TechType, StudyItemManager> techTypeDictionary = new Dictionary<TechType, StudyItemManager>();//科技的管理层


    public Dictionary<TechType, bool> techTypeStudyFlag;//科技是否研究

    private void Start()
    {
        Instance = this;
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


    }

    // 获取科技脚本实例
    public StudyItemManager GetTech(TechType techType)
    {
        return techTypeDictionary[techType];
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