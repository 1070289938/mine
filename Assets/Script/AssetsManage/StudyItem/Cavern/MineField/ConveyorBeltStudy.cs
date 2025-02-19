using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//矿石传送带的研究
public class ConveyorBeltStudy : MonoBehaviour
{


    string studyName = "矿石传送带";

    string details = "在矿车上面配备一条矿石传送带,提升矿车的运输效率\n\n矿车对工人的提升增加100%";

    string Successful = "矿石传送带研究成功!";

    TechType techType = TechType.ConveyorBelt;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币100k ，铜矿10k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("100k"),
        [ResourceType.Copper] = 10000,
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
        if (TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel) &&
        TechManager.Instance.GetTechFlag(TechType.tramcar))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        //提升100%矿车效率
        ResourceAdditionManager.Instance.AddMineBonus(1);
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
