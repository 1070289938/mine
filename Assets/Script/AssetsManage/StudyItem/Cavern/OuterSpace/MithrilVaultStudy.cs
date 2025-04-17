using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//秘银金库
public class MithrilVaultStudy : MonoBehaviour
{


    string studyName = "秘银金库";

    string details = "给银行的金库用秘银进行强化使软妹币的储存效率更高\n\n银行的储量提升25%";

    string Successful = "秘银金库研究成功";

    TechType techType = TechType.MithrilVault;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("1G"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("305K"),
         [ResourceType.Mithril] = AssetsUtil.ParseNumber("580"),

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
      
        ResourceAdditionManager.Instance.AddBankReserveSurcharge(0.25);

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
