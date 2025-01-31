using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechManager : MonoBehaviour
{
    public static TechManager Instance { get; private set; }

    public GameObject content;

    public Dictionary<TechType, StudyItemManager> techTypeDictionary = new Dictionary<TechType, StudyItemManager>();//科技的管理层


    public Dictionary<TechType, bool> techTypeStudyFlag = new Dictionary<TechType, bool>();//科技是否已研究

    private void Start()
    {
        Instance = this;
        StudyItemManager[] studyItems = content.GetComponentsInChildren<StudyItemManager>(true);
     
        foreach(StudyItemManager itemManager in studyItems){
            techTypeDictionary[itemManager.techType] = itemManager;

            techTypeStudyFlag[itemManager.techType] = false;//默认所有科技未研究
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
        return techTypeStudyFlag[techType];
    }
}