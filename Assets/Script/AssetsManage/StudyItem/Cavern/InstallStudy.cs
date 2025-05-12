using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 初始化科技
/// </summary>
public class InstallStudy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InstallStudyItem[] studyItems = GetComponentsInChildren<InstallStudyItem>();
        foreach (InstallStudyItem item in studyItems)
        {
            item.Install();
        }
        //初始化整理科技
        TechManager.Instance.Install();
        Debug.Log("--------科技初始化完成--------");

        SaveLoadManager.Instance.TryLoadGame();
        MonitoringTechnology();


    }
    void MonitoringTechnology()
    {
        //判断如果前置资源拥有就直接后置
        foreach (TechnologyBean bean in DataProcessing.technologies)
        {
            List<ResourceType> resourceTypes = bean.advanceResources;
            if (resourceTypes != null)
            {
                if (resourceTypes.Count != 0)
                {
                    if (ResourceManager.Instance.IsResourceUnlocked(resourceTypes[0]))
                    {
                        //如果前置资源解锁了
                        if (!TechManager.Instance.GetTechFlag(bean.techType))
                        {
                            TechChecker.Instance.AddCheckTech(bean.techType);
                        }

                    }
                }
            }
        }
        //判断如果前置科技研究了就直接后置
        foreach (TechnologyBean bean in DataProcessing.technologies)
        {
            List<TechType> techTypes = bean.advanceTechType;
            if (techTypes != null)
            {
                if (techTypes.Count != 0)
                {
                    if (TechManager.Instance.GetTechFlag(techTypes[0]))
                    {
                        //如果前置科技解锁了
                        if (!TechManager.Instance.GetTechFlag(bean.techType))
                        {
                            TechChecker.Instance.AddCheckTech(bean.techType);
                        }

                    }
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
