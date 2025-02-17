using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//铜镐的研究
public class InspectWonderfulRodStudy : MonoBehaviour
{


    string studyName = "检查奇怪的金属棒";

    string details = "这根金属棒十分高大粗犷大致有百米高十几米宽,从矿洞底部一直衍生到矿洞顶部";

    string Successful = "我们检查了一下奇怪的金属棒之后,发现以目前科技无法研究金属棒,蛮力派的员工建议你强行分解这根金属棒获取巨额的钢资源,而知识派的员工建议在未来研究一下这个金属棒!";

    TechType techType = TechType.InspectWonderfulRod;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        [ResourceType.Currency] = 10000
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
        //发现金属棒
        if (TechManager.Instance.GetTechFlag(TechType.MetalBarFound))
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
