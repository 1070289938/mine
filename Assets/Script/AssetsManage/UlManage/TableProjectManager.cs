using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class TableProjectManager : MonoBehaviour
{

    public Transform content;

    public GameObject hiddenContent;

    public GameObject table;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChildContentShow();

        ContentShow();

        
    }
    //判断是否显示研究
    void ContentShow(){
        if(hiddenContent.activeSelf){
                    //显示状态下如果没有就隐藏，
            if(AreAllChildrenHidden(content)){
                        hiddenContent.SetActive(false);
                        table.SetActive(false);
                }
        }else{
                    //显示状态下如果有就显示，
            if(!AreAllChildrenHidden(content)){
                        hiddenContent.SetActive(true);
                        table.SetActive(true);
            }
        }

    }


    void ChildContentShow(){
        
        ProjectManager[] projects = content.GetComponentsInChildren<ProjectManager>(true);
        foreach(ProjectManager project in projects){

            if(hiddenContent.activeSelf){
                    //显示状态下如果没有就隐藏，
                if(AreAllChildrenHidden(project.content.transform)){
                        project.gameObject.SetActive(false);
                }
            }else{
                    //显示状态下如果有就显示，
                if(!AreAllChildrenHidden(project.content.transform)){
                        project.gameObject.SetActive(true);
                }
        }

        }

    }






bool AreAllChildrenHidden(Transform parent)
{
    for (int i = 0; i < parent.childCount; i++)
    {
        Transform child = parent.GetChild(i);
        if (child.gameObject.activeSelf) // 检查是否处于激活状态
        {
            return false; // 如果有一个子节点是激活状态，返回 false
        }
    }
    return true; // 全部子节点都是隐藏状态，返回 true
}

    
}
