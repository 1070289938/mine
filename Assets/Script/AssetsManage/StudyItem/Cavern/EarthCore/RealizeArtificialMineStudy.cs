using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//实施人造矿井
public class RealizeArtificialMineStudy : MonoBehaviour
{


    string studyName = "实施人造矿井";

    string details = "人造矿井需要的特殊材料,钨已经有了,现在可以在工厂附近建造人造矿井来提升工厂的效率\n\n解锁人造矿井";

    string Successful = "实施人造矿井研究成功";

    TechType techType = TechType.RealizeArtificialMine;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("23M"),
        [ResourceType.Science] = 5000,
        [ResourceType.Tungsten] = AssetsUtil.ParseNumber("100"),

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
        //钨矿采集器
        if (TechManager.Instance.GetTechFlag(TechType.TungstenHarvester))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.ArtificialMine);
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
