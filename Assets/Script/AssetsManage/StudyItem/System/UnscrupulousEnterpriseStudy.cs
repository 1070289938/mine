using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//黑心企业的研究
public class UnscrupulousEnterpriseStudy : MonoBehaviour
{


    string studyName = "黑心企业";

    string details = "提升软妹币25%总产量";

    string Successful = "黑心企业研究成功!";

    TechType techType = TechType.UnscrupulousEnterprise;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.RegeneratedCrystal] = 2000,
        [ResourceType.DimensionalStone] = 250
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
        //需要重生水晶
        if (TechManager.Instance.GetTechFlag(TechType.Entrepreneur))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {

        ResourceAdditionManager.Instance.AddRMBboost(0.5);

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
