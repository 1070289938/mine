using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//四维制品的研究
public class DimensionalProductStudy : MonoBehaviour
{


    string studyName = "四维制品";

    string details = "制造工人效率+80%";

    string Successful = "四维制品研究成功!";

    TechType techType = TechType.dimensionalProduct;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 666,
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
        //一丝不苟
        //四维宝石
        if (TechManager.Instance.GetTechFlag(TechType.meticulous) &&
        ResourceManager.Instance.IsResourceUnlocked(ResourceType.DimensionalStone))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        ResourceAdditionManager.Instance.AddFabricator(0.8);

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
