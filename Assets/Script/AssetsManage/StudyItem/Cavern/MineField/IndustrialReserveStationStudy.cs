using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//工业储备站的研究
public class IndustrialReserveStationStudy : MonoBehaviour
{


    string studyName = "工业储备站";

    string details = "普通的仓库无法储存工业材料,工业储备站可以提升工业材料的储量\n\n解锁工业储备站";

    string Successful = "工业储备站研究成功!";

    TechType techType = TechType.IndustrialReserveStation;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币50k ，钢10
        [ResourceType.Currency] = AssetsUtil.ParseNumber("50k"),
        [ResourceType.Steel] = 10,
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
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.IndustrialReserveStation);
        facility.gameObject.SetActive(true);
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
