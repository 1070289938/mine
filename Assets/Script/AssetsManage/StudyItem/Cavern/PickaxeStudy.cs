using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//石镐的研究
public class PickaxeStudy : MonoBehaviour
{
   

    string studyName = "研究石镐";

    string details = "将尖锐的石头捆绑在一起制作成石镐\n\n将所有的工具替换为石镐\n\n提升10%采矿效率";

    string Successful = "石镐研究成功!";

    TechType techType = TechType.pickaxe;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>(); //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;
    
   

    void Awake()
    {   
        studyItemManager = GetComponent<StudyItemManager>();
        //设置价格
        resources[ResourceType.Stone]=10;
        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName,details,resources,techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study=Study;
    }
    
    void Inspect(){
        //如果有石头就解锁这个科技
        if(ResourceManager.Instance.IsResourceUnlocked(ResourceType.Stone)){
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
              
            TechChecker.Instance.AddCheckTech(TechType.BrassPick);//铜镐
        }
    }
    //研究按钮事件
    void Study(){
        //触发研究事件

        LogManager.Instance.AddLog(Successful);
        //增加10%工具效率
        ResourceAdditionManager.Instance.AddTool(0.1);

        //概率挖出铜
        PileOfOreManager pileOfOre = FacilityManager.Instance.GetFacilityPanel(FacilityType.Ore).GetComponent<PileOfOreManager>();
        pileOfOre.AddCopper();

    }



    //点击事件
    void onClick(){
        studyItemManager.ShowItemMsg();

    }


}
