using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//回转炉的研究
public class RotaryFurnaceStudy : MonoBehaviour
{


    string studyName = "回转炉";

    string details = "回转炉可以大幅的提升铜矿的产量\n\n提升铜矿50%的产量";

    string Successful = "回转炉研究成功!";

    TechType techType = TechType.RotaryFurnace;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币5000 ，钢300
        [ResourceType.Currency] = 5000,
        [ResourceType.Steel] = 300,
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
        //高炉
        if (TechManager.Instance.GetTechFlag(TechType.BlastFurnace))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddMinerCopper(0.5);
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
