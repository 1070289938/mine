using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//派遣侦察队
public class DispatchReconnaissancePartyStudy : MonoBehaviour
{


    string studyName = "派遣侦察队";

    string details = "组织一队侦察队来对地心犬聚集地进行探测";

    string Successful = "据侦察队的汇报,表示发现中心有一个形状十分扭曲的四维传送门,猜测这里的所有地心犬都是从这里钻出来的,而侦察队再也没有回来......";

    TechType techType = TechType.DispatchReconnaissanceParty;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("150M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("80k"),

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
        //继续深入
        if (TechManager.Instance.GetTechFlag(TechType.GoDeeper))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

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
