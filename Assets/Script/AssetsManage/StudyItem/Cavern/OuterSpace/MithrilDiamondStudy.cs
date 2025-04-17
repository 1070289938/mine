using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//秘银钻
public class MithrilDiamondStudy : MonoBehaviour
{


    string studyName = "秘银钻";

    string details = "秘银钻的挖掘效率更高\n\n挖矿效率提升35%";

    string Successful = "秘银钻研究成功";

    TechType techType = TechType.MithrilDiamond;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("655M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
         [ResourceType.Mithril] = AssetsUtil.ParseNumber("280"),

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
        //秘银锻造
        if (TechManager.Instance.GetTechFlag(TechType.MithrilForging))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
      
        ResourceAdditionManager.Instance.AddTool(0.35);

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
