using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钢筋混凝土的研究
public class ReinforcedConcreteStudy : MonoBehaviour
{


    string studyName = "钢筋混凝土";

    string details = "给房屋换上钢筋混凝土,可以使房屋更加的坚固舒适\n\n提升房屋30%软妹币产量";

    string Successful = "钢筋混凝土研究成功!";

    TechType techType = TechType.ReinforcedConcrete;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币80k ，钢500
        [ResourceType.Currency] = AssetsUtil.ParseNumber("80k"),
        [ResourceType.Steel] = 500,
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
        //水泥房屋
        if (TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel) &&
        TechManager.Instance.GetTechFlag(TechType.ConcreteBuilding))
        {
            Debug.Log("解锁钢筋混凝土");
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //房屋坚固程度提升30%
        ResourceAdditionManager.Instance.AddTenementComfort(0.3);
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
