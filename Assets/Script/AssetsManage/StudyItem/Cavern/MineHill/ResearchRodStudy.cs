using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//研究金属棒的研究
public class ResearchRodStudy : MonoBehaviour
{


    string studyName = "研究金属棒";

    string details = "花费巨资召集各地科学家研究这个神奇的金属棒";

    string Successful = "科学家们发现这个金属棒由含有未知的元素科学家将其命名为佐里旬,这个元素可以压缩附近区域的所有物质,而当远离了物质之后,物质将会回到原始的大小,是一个超越常理的神奇物质";

    TechType techType = TechType.ResearchRod;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币20000k 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("2000k"),
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
        //实验室
        if (TechManager.Instance.GetTechFlag(TechType.Laboratory))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        // //去除特殊内强制分解金属棒的选项
        // SpecialPanelManager specialPanelManager = TechManager.Instance.GetSpecialTech(SpecialOptionType.ForcedDecomposition);
        // //隐藏选项
        // specialPanelManager.gameObject.SetActive(false);

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
