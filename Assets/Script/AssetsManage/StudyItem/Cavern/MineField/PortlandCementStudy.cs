using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//硅酸盐水泥的研究
public class PortlandCementStudy : MonoBehaviour
{


    string studyName = "硅酸盐水泥";

    string details = "在水泥中混一些硅酸盐可以更加有效的生产水泥\n\n水泥的生产效率提升100%";

    string Successful = "硅酸盐水泥研究成功!";

    TechType techType = TechType.PortlandCement;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币150k ，铜矿1000
        [ResourceType.Currency] = AssetsUtil.ParseNumber("150k"),
        [ResourceType.Copper] = 1000,
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
        //硅
        if (TechManager.Instance.GetTechFlag(TechType.SiliconMining))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddCement(1);
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
