using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//画饼的研究
public class PaintedStudy : MonoBehaviour
{


    string studyName = "给员工画饼";

    string details = "只要坚持努力挖矿，未来的星辰大海，都会刻上你的名字！\n\n提升矿洞内员工20%工作效率";

    string Successful = "画饼研究成功!";

    TechType techType = TechType.painted;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Currency] = AssetsUtil.ParseNumber("25k");
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //石工
        //铜工
        //铁工
        //水泥工
        if (TechManager.Instance.GetTechFlag(TechType.StoneMiner) &&
         TechManager.Instance.GetTechFlag(TechType.CopperMiner) &&
          TechManager.Instance.GetTechFlag(TechType.IronMiner) &&
           TechManager.Instance.GetTechFlag(TechType.CementManufacture))
        {

            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

            TechChecker.Instance.AddCheckTech(TechType.BrokenStone);//破开石头
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //提升员工20%工作效率
        ResourceAdditionManager.Instance.AddWorker(0.2);
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
