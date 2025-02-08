using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铁质大锤的研究
public class ChiselStudy : MonoBehaviour
{


    string studyName = "研究凿子";

    string details = "凿子可以有效的破碎大型石头\n\n提升40%石头采集效率";

    string Successful = "凿子研究成功!";

    TechType techType = TechType.Chisel;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;
        //设置价格
        resources[ResourceType.Currency] = 5000;
        resources[ResourceType.Iron] = 300;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //铁镐
        if (TechManager.Instance.GetTechFlag(TechType.tupid))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        //提升40%矿工石头产出效率
        ResourceAdditionManager.Instance.AddMinerStone(0.4);
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
