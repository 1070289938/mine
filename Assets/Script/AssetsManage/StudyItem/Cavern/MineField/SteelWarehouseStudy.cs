using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//钢制仓库的研究
public class SteelWarehouseStudy : MonoBehaviour
{


    string studyName = "钢制仓库";

    string details = "钢制仓库可以更高效的储存资源\n\n提升30%仓库储量";

    string Successful = "钢制仓库研究成功!";

    TechType techType = TechType.SteelWarehouse;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new()
    {
        //设置价格 软妹币30k ，钢 200
        [ResourceType.Currency] = AssetsUtil.ParseNumber("45k"),
        [ResourceType.Steel] = 200,
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
        //钢铁
        if (TechManager.Instance.GetTechFlag(TechType.RefinedIronSteel))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        
        //提升仓库30%储量上限
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);

        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(0.3);
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
