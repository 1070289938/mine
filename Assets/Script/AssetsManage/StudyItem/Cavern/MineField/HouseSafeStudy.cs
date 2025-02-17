using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//房屋保险箱的研究
public class HouseSafeStudy : MonoBehaviour
{


    string studyName = "房屋保险箱";

    string details = "给每个房屋都装上坚硬的保险箱\n\n提升房屋30%软妹币储量";

    string Successful = "房屋保险箱研究成功!";

    TechType techType = TechType.HouseSafe;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币5000 ，钢200
        [ResourceType.Currency] = 5000,
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
        //炼制钢铁
        //存钱箱
        if (TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel) &&
        TechManager.Instance.GetTechFlag(TechType.MoneyBox))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddTenementSaveMoney(0.3);
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
