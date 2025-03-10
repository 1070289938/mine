using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//探索矿山的研究
public class ExploratoryMineStudy : MonoBehaviour
{


    string studyName = "探索山脉";

    string details = "你的直觉认为这座山脉内一定有一些更加稀有的资源,所以决定花费巨资去探索这片山脉";

    string Successful = "你发现这是一座存有大量稀有资源的矿山,并找到了钛，铝这些稀有金属";

    TechType techType = TechType.ExploratoryMine;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {

        [ResourceType.Currency] = AssetsUtil.ParseNumber("300k")
    }; //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
      
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //发现山脉
        if (TechManager.Instance.GetTechFlag(TechType.DiscoveredMine))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
  
    }


    // Update is called once per frame
    void Update()
    {

    }

    //点击事件
    void onClick()
    {
        studyItemManager.ShowItemMsg();

    }


}
