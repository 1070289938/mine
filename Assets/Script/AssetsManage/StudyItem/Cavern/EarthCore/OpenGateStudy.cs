using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//研究大门
public class OpenGateStudy : MonoBehaviour
{


    string studyName = "研究大门";

    string details = "大门看起来十分的封闭,十分的庞大,像是某个时代的奇迹巨构,你出于好奇,让科学家们研究如何开启这座大门";

    string Successful = "经过科学家们一顿花哨的操作后,大门自动的缓慢开启,里面渗出极其浓厚的血腥味让人发呕,同时冒出一阵阵的寒风,在地心如此炎热的地方居然会有寒风的存在";

    TechType techType = TechType.OpenGate;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("35M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("50k"),

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
        //找到大门
        if (TechManager.Instance.GetTechFlag(TechType.DiscoverGate))
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
