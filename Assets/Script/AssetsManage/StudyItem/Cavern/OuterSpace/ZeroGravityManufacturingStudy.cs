using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//零重力制造业
public class ZeroGravityManufacturingStudy : MonoBehaviour
{


    string studyName = "零重力制造业";

    string details = "可以在太空进行制作物品,零重力环境可以让精良部件制造的更加轻松\n\n制作工人效率提升30%";

    string Successful = "零重力制造业研究成功";

    TechType techType = TechType.ZeroGravityManufacturing;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 2G 科技点190K
        [ResourceType.Currency] = AssetsUtil.ParseNumber("655M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("275K"),

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
        //人造卫星
        if (TechManager.Instance.GetTechFlag(TechType.ArtificialSatellite))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
      
        ResourceAdditionManager.Instance.AddFabricator(0.3);

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
