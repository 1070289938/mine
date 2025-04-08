using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//敬拜的研究
public class WorshipStudy : MonoBehaviour
{


    string studyName = "敬拜";

    string details = "寺庙的效果翻倍";

    string Successful = "敬拜研究成功!";

    TechType techType = TechType.Worship;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 1500,
        [ResourceType.DimensionalStone] = 50
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
        //四维宝石
        //信仰
        if (ResourceManager.Instance.IsResourceUnlocked(ResourceType.DimensionalStone) &&
        TechManager.Instance.GetTechFlag(TechType.Faith))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {

        ResourceAdditionManager.Instance.AddTemple(1);

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
