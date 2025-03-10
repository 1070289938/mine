using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//金库的研究
public class TreasuryStudy : MonoBehaviour
{


    string studyName = "金库";

    string details = "给所有的房屋都安装一个小型金库\n\n提升所有房屋50%软妹币储量";

    string Successful = "金库研究成功!";

    TechType techType = TechType.Treasury;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币100k ，钢200
        [ResourceType.Currency] = AssetsUtil.ParseNumber("100k"),
        [ResourceType.Steel] = 200,
    };
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
        //银行
        if (TechManager.Instance.GetTechFlag(TechType.Bank))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddTenementSaveMoney(0.5);
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();
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
