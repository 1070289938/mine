using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//机械外骨骼的研究
public class MechanicalExoskeletonStudy : MonoBehaviour
{


    string studyName = "机械外骨骼";

    string details = "机械外骨骼由坚硬的合金组成,拥有超高的强度并且便于携带,可以大幅度的提升矿工的工作效率\n\n提升40%所有采矿工人的效率";

    string Successful = "机械外骨骼研究成功!";

    TechType techType = TechType.MechanicalExoskeleton;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币2500k 合金300
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2500k"),
        [ResourceType.Alloy] = AssetsUtil.ParseNumber("300"),
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
        //自动化
        if (TechManager.Instance.GetTechFlag(TechType.Automate))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        ResourceAdditionManager.Instance.AddMiningWorker(0.4);
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
