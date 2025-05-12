using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechManager : MonoBehaviour
{
    public static TechManager Instance { get; private set; }

    public GameObject content;//Tech科技
    public GameObject SystemContent;//系统科技

    public GameObject Special;//特殊科技

    public Dictionary<TechType, StudyItemManager> techTypeDictionary = new Dictionary<TechType, StudyItemManager>();//科技的管理层

    public Dictionary<SpecialOptionType, SpecialPanelManager> specialPanelManagerMap = new();//特殊科技的管理层


    public Dictionary<TechType, bool> specialTech = new()
    {   //系统科技

        [TechType.SpaceStorage] = true,//空间储物
        [TechType.SpatialAdvantage] = true,//空间优势
        [TechType.SpatialHegemony] = true,//空间霸权
        [TechType.DimMemory] = true,//模糊的记忆
        [TechType.MemoriesPastLives] = true,//前世的回忆
        [TechType.PastMemory] = true,//昔日的记忆
        [TechType.SacredMemory] = true,//神圣的回忆
        [TechType.TheThoughts] = true,//极耀圣念
        [TechType.SpatioMemory] = true,//时空记忆
        [TechType.MuscleStrengthening] = true,//肌肉强化
        [TechType.Artisanship] = true,//技工天赋
        [TechType.AttentionDetail] = true,//注重细节
        [TechType.meticulous] = true,//一丝不苟
        [TechType.dimensionalProduct] = true,//四维制品
        [TechType.BusinessAcumen] = true,//商业头脑
        [TechType.Entrepreneur] = true,//企业家
        [TechType.DimensionalWarehouse] = true,//四维仓库
        [TechType.MultidimensionalWarehouse] = true,//多维仓库
        [TechType.Faith] = true,//信仰
        [TechType.Worship] = true,//敬拜
        [TechType.IntelligentMind] = true,//聪明头脑
        [TechType.ScientificTalent] = true,//科学天赋
        [TechType.BornScientist] = true,//天生科学家

    };

    public Dictionary<TechType, bool> techTypeStudyFlag;//科技是否研究


    void Awake()
    {
        Instance = this;
        ///特殊科技
        SpecialPanelManager[] SpecialStudyItems = Special.GetComponentsInChildren<SpecialPanelManager>(true);
        foreach (SpecialPanelManager itemManager in SpecialStudyItems)
        {
            specialPanelManagerMap[itemManager.specialOptionType] = itemManager;
        }
    }

    public void Install()
    {
        ///普通科技
        StudyItemManager[] studyItems = content.GetComponentsInChildren<StudyItemManager>(true);
        if (techTypeStudyFlag == null)
        {
            techTypeStudyFlag = new Dictionary<TechType, bool>();
        }
        foreach (StudyItemManager itemManager in studyItems)
        {
            techTypeDictionary[itemManager.techType] = itemManager;
        }


        ///系统科技
        StudyItemManager[] systemStudyItems = SystemContent.GetComponentsInChildren<StudyItemManager>(true);
        foreach (StudyItemManager itemManager in systemStudyItems)
        {
            techTypeDictionary[itemManager.techType] = itemManager;
            TechChecker.Instance.AddCheckMethod(itemManager.InspectFrame);//直接监听所有科技
        }
        ListenSpecific();
        Debug.Log("--------科技缓存赋值完成--------");
    }

    List<TechType> specific = new()
    {
        TechType.InspectWonderfulRod,
        TechType.ExploratoryMine,
        TechType.SoundingDepth,
        TechType.trace,
        TechType.OpenGate,
         TechType.NearSpaceExploration,
         TechType.ExperimentalExplorer,
          TechType.CalculatingJupiterOrbit,
    };

    /// <summary>
    /// 自动监听特定科技
    /// </summary>
    void ListenSpecific()
    {
        foreach (TechType type in specific)
        {
            TechChecker.Instance.AddCheckTech(type);//直接监听所有指定
        }

    }




    private void Start()
    {




    }

    // 获取科技脚本实例
    public StudyItemManager GetTech(TechType techType)
    {
        return techTypeDictionary[techType];
    }


    // 获取特殊科技实例
    public SpecialPanelManager GetSpecialTech(SpecialOptionType techType)
    {
        return specialPanelManagerMap[techType];
    }




    /// <summary>
    /// 获取科技是否研究
    /// </summary>
    /// <param name="techType"></param>
    /// <returns></returns>
    public bool GetTechFlag(TechType techType)
    {
        if (techTypeStudyFlag == null)
        {
            return false;
        }
        if (techTypeStudyFlag.ContainsKey(techType))
        {
            return techTypeStudyFlag[techType];
        }
        return false;
    }
}