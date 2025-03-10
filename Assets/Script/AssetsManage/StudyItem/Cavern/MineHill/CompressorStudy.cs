using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//压缩增幅器的研究
public class CompressorStudy : MonoBehaviour
{


    string studyName = "压缩增幅器";

    string details = "压缩增幅器可以进一步的提升集装箱的压缩效率\n\n解锁压缩增幅器";

    string Successful = "压缩增幅器研究成功!";

    TechType techType = TechType.Compressor;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币3M 
        [ResourceType.Currency] = AssetsUtil.ParseNumber("3M"),
        [ResourceType.Zorizun] = AssetsUtil.ParseNumber("50"),
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
        //物质压缩
        if (TechManager.Instance.GetTechFlag(TechType.MaterialCompression))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        FacilityPanelManager facility = FacilityManager.Instance.GetFacilityPanel(FacilityType.MaterialCompressor);
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
