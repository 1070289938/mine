using System;
using System.Collections.Generic;
using UnityEngine;

public class TechChecker : MonoBehaviour
{
    // 用于存储检查逻辑的方法列表
    public List<Action> checkMethods = new List<Action>();




    public static TechChecker Instance { get; private set; }


    void Awake()
    {

        Instance = this;


    }


    // 每帧调用的逻辑
    void Update()
    {
        if (checkMethods != null)
        {
            for (int i = 0; i < checkMethods.Count; i++)
            {
                checkMethods[i]?.Invoke(); // 执行每个检查方法
            }
        }

    }

    // 添加检查方法
    public void AddCheckMethod(Action checkMethod)
    {


        if (!checkMethods.Contains(checkMethod))
        {
            checkMethods.Add(checkMethod);
        }
    }




    // 添加检查科技
    public void AddCheckTech(TechType type)
    {
        StudyItemManager studyItem = TechManager.Instance.GetTech(type);
        if (studyItem != null)
        {
            AddCheckMethod(studyItem.InspectFrame);
        }

    }


    // 移除检查方法
    public void RemoveCheckMethod(Action checkMethod)
    {
        if (checkMethods.Contains(checkMethod))
        {
            checkMethods.Remove(checkMethod);
        }
    }



    // 移除检查科技
    public void RemoveCheckTech(TechType type)
    {
        StudyItemManager studyItem = TechManager.Instance.GetTech(type);
        if (studyItem != null)
        {
            RemoveCheckMethod(studyItem.InspectFrame);
        }

    }

}
